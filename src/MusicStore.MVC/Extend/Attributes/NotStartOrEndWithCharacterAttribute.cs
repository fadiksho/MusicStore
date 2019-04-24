using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Extend.Attributes
{
  public class NotStartOrEndWithCharacterAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var valueToCheck = (string)value;

      if(!string.IsNullOrWhiteSpace(valueToCheck) &&
        char.IsLetterOrDigit(valueToCheck[0]) && 
        char.IsLetterOrDigit(valueToCheck[valueToCheck.Length - 1]))
      {
        return ValidationResult.Success;
      }
      return new ValidationResult(ErrorMessage);
    }
  }
}
