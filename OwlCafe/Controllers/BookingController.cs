using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.DTOs;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRestaurantTableRepository _tableRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<BookingDto> _validator;
        private readonly ILogger<BookingController> _logger;

        public BookingController(
            IBookingRepository bookingRepository,
            IRestaurantTableRepository tableRepository,
            IMapper mapper,
            IValidator<BookingDto> validator,
            ILogger<BookingController> logger)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingDto bookingDto)
        {
            try
            {
                bookingDto.BookingDate = NormalizeBookingDate(bookingDto.BookingDate);

                // Validate
                var validationResult = await _validator.ValidateAsync(bookingDto);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View(bookingDto);
                }

                // Check for duplicate booking
                if (bookingDto.TableId.HasValue)
                {
                    var isAvailable = await _bookingRepository.IsTableAvailableAsync(
                        bookingDto.TableId.Value,
                        bookingDto.BookingDate,
                        bookingDto.BookingTime);

                    if (!isAvailable)
                    {
                        ModelState.AddModelError("", "This table is not available for the selected date and time.");
                        return View(bookingDto);
                    }
                }

                var booking = _mapper.Map<Booking>(bookingDto);
                booking.Status = BookingStatus.Pending;
                booking.CreatedDate = DateTime.UtcNow;

                await _bookingRepository.AddAsync(booking);
                await _bookingRepository.SaveChangesAsync();

                _logger.LogInformation($"New booking created: {booking.Id}");
                return RedirectToAction("Confirmation", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                ModelState.AddModelError("", "An error occurred while processing your booking. Please try again.");
                return View(bookingDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _bookingRepository.GetBookingWithTableAsync(id);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

        [HttpGet]
        public IActionResult Status()
        {
            return View(new BookingStatusLookupDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Status(BookingStatusLookupDto lookup)
        {
            if (!ModelState.IsValid)
                return View(lookup);

            var booking = await _bookingRepository.GetBookingWithTableAsync(lookup.BookingId);
            if (booking == null || !MatchesBookingContact(booking, lookup.Contact))
            {
                ModelState.AddModelError("", "We could not find a booking with that confirmation number and contact detail.");
                return View(lookup);
            }

            return View("StatusResult", booking);
        }

        private static bool MatchesBookingContact(Booking booking, string contact)
        {
            var normalizedContact = contact.Trim();
            return string.Equals(booking.Email, normalizedContact, StringComparison.OrdinalIgnoreCase)
                || string.Equals(booking.Phone, normalizedContact, StringComparison.OrdinalIgnoreCase);
        }

        private static DateTime NormalizeBookingDate(DateTime date)
        {
            return DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        }

        [HttpGet("api/available-tables")]
        public async Task<IActionResult> GetAvailableTables(DateTime date, TimeSpan time, int guests)
        {
            try
            {
                var tables = await _tableRepository.GetTablesByCapacityAsync(guests);
                return Json(tables);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available tables");
                return BadRequest();
            }
        }
    }
}
