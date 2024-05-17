using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MinimalAPI_Task.Models;

namespace MinimalAPI_Task.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Must(BeValidName)
                .WithMessage("FirstName can not containg numbers");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .Must(BeValidName)
                .WithMessage("LastName can not containg numbers");

            RuleFor(p => p.BirthDate)
                .Must(BeAValidDate)
                .WithMessage("BirthDate must be in the format YYYY-MM-DD.");

            RuleFor(p => p.Address)
                .SetValidator(new AddressValidation());
        }

        private bool BeAValidDate(DateOnly date)
        {
            if (date == default)
            {
                return false;
            }
            return true;
        }
        private bool BeValidName(string name)
        {
            if (name == null)
                return false;

            return !name.Any(char.IsDigit);
        }

    }
}
