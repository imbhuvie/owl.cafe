using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRestaurantTableRepository _tableRepository;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(
            IBookingRepository bookingRepository,
            IRestaurantTableRepository tableRepository,
            ILogger<BookingsController> logger)
        {
            _bookingRepository = bookingRepository;
            _tableRepository = tableRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? status = null)
        {
            try
            {
                IEnumerable<Booking> bookings;

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<BookingStatus>(status, out var bookingStatus))
                {
                    bookings = await _bookingRepository.GetBookingsByStatusAsync(bookingStatus);
                }
                else
                {
                    bookings = await _bookingRepository.GetAllAsync();
                }

                return View(bookings.OrderByDescending(b => b.CreatedDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading bookings");
                return View(new List<Booking>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingRepository.GetBookingWithTableAsync(id);
            if (booking == null)
                return NotFound();

            var tables = await _tableRepository.GetAllAsync();
            ViewData["Tables"] = tables;

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id)
                return NotFound();

            try
            {
                var existingBooking = await _bookingRepository.GetByIdAsync(id);
                if (existingBooking == null)
                    return NotFound();

                existingBooking.Status = booking.Status;
                existingBooking.TableId = booking.TableId;
                existingBooking.SpecialRequest = booking.SpecialRequest;
                existingBooking.UpdatedDate = DateTime.UtcNow;

                _bookingRepository.Update(existingBooking);
                await _bookingRepository.SaveChangesAsync();

                TempData["Success"] = "Booking updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking");
                var tables = await _tableRepository.GetAllAsync();
                ViewData["Tables"] = tables;
                ModelState.AddModelError("", "Error updating booking");
                return View(booking);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, BookingStatus status)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(id);
                if (booking == null)
                    return NotFound();

                booking.Status = status;
                booking.UpdatedDate = DateTime.UtcNow;

                _bookingRepository.Update(booking);
                await _bookingRepository.SaveChangesAsync();

                TempData["Success"] = $"Booking status changed to {status}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing booking status");
                TempData["Error"] = "Error changing booking status";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(id);
                if (booking == null)
                    return NotFound();

                _bookingRepository.Remove(booking);
                await _bookingRepository.SaveChangesAsync();

                TempData["Success"] = "Booking deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking");
                TempData["Error"] = "Error deleting booking";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
