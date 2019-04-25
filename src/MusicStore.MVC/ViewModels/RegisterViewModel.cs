using MusicStore.MVC.Extend.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "Username")]
    [NotStartOrEndWithCharacter(ErrorMessage = "Username should not start or end with character.")]
    [RegularExpression(@"^[a-zA-Z0-9]+$|[a-zA-Z0-9]+[-_]+[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters, digits or the characters ( -_ ).")]
    [StringLength(maximumLength: 15, MinimumLength = 2)]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "E-Mail")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "Password must be 8 long and maximum 80 long.")]
    public string Password { get; set; }

    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public string Message { get; set; }
  }
}
