using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Helpers;
public class EndDateAfterStartDateAttribute : ValidationAttribute
{
    private readonly string _startDatePropertyName;

    public EndDateAfterStartDateAttribute(string startDatePropertyName)
    {
        _startDatePropertyName = startDatePropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
        if (startDateProperty == null)
        {
            return new ValidationResult($"'{_startDatePropertyName}' alanı bulunamadı.");
        }

        var startDateValue = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);
        var endDateValue = value as DateTime?;

        if (endDateValue.HasValue && endDateValue <= startDateValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}