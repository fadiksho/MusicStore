using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.ViewModels
{
  public class LogInViewModel
  {
    [Required]
    [Display(Name = "E-Mail")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }

    public bool RememberMe { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "Password must be 8 long and maximum 80 long.")]
    public string Password { get; set; }

    public string Message { get; set; }
  }
}
