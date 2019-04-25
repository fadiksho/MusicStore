using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.ViewModels
{
  public class EmailConfirmationViewModel
  {
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public bool IsConfirmationLinkSent { get; set; }
    public string Message { get; set; }
  }
}