using FluentValidation;
using OwlCafe.DTOs;

namespace OwlCafe.Validators
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required")
                .MinimumLength(3).WithMessage("Customer name must be at least 3 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\d{10,}$").WithMessage("Phone number must be at least 10 digits");

            RuleFor(x => x.BookingDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Booking date must be today or in the future");

            RuleFor(x => x.Guests)
                .GreaterThan(0).WithMessage("Number of guests must be greater than 0")
                .LessThanOrEqualTo(20).WithMessage("Maximum 20 guests per booking");
        }
    }
}
