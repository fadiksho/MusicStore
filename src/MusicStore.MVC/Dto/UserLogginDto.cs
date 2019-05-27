using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Dto
{
  public class UserLogginDto
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "Password must be 8 long and maximum 80 long.")]
    public string Password { get; set; }
  }
}
