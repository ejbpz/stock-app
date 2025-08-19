using System.ComponentModel.DataAnnotations;

namespace StockApp.Models.Validations
{
    public class MinimumDate : ValidationAttribute
    {
        private readonly DateTime _minimumDateTime;

        public MinimumDate(int year, int month, int day)
        {
            _minimumDateTime = DateTime.Parse($"{year}-{month}-{day}");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.TryParse(Convert.ToString(value), out DateTime actualValue))
            {
                if (actualValue < _minimumDateTime) return new ValidationResult("The given date is earlier than the minimum date.");

                return ValidationResult.Success;
            }
            return new ValidationResult("There's not a valid date supplied.");
        }
    }
}
