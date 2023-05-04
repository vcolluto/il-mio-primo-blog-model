using System.ComponentModel.DataAnnotations;

namespace NetCore_01.Validations
{
    public class FirstLetterIsUppercaseValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value!=null && (((string)value)[0].ToString().ToUpper() != (((string)value)[0].ToString())))
                return new ValidationResult("La prima lettera deve essere maiuscola");
            return ValidationResult.Success;
        }
    }
}
