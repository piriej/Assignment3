using System.Windows.Controls;

namespace Library.Features
{
    public class StringToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int validatedNumber;
            return value != null && int.TryParse(value.ToString(), out validatedNumber) 
                ? new ValidationResult(true, null) 
                : new ValidationResult(false, "The code must be a valid number");
        }
    }
}
