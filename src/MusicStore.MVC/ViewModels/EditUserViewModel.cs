using MusicStore.MVC.Models;
using System.Collections.Generic;

namespace MusicStore.MVC.ViewModels
{
  public class EditUserViewModel
  {
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<CheckBoxItem> UserRoles { get; set; }
      = new List<CheckBoxItem>();

    public string Message { get; set; }
  }
}
