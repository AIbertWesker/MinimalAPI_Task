using FluentValidation;
using MinimalAPI_Task.Models;

namespace MinimalAPI_Task.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.City)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(a => a.Street)
                .MaximumLength(50);

            RuleFor(a => a.BuildingNumber)
                .GreaterThan(0);

            RuleFor(a => a.ApartmentNumber)
                .GreaterThan(0);

            RuleFor(a => a.ZipCode)
                .NotEmpty()
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("ZipCode must be in the format XX-XXX.");
        }
    }
}

