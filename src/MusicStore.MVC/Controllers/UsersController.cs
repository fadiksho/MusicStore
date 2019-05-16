using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.MVC.Models;
using MusicStore.MVC.ViewModels;
using System;
using System.Threading.Tasks;

namespace MusicStore.MVC.Controllers
{
  [Authorize]
  public class UsersController : Controller
  {
    private readonly UserManager<User> userManager;

    public UsersController(
      UserManager<User> userManager)
    {
      this.userManager = userManager;
    }

    public async Task<IActionResult> Profile()
    {
      try
      {
        var user = await userManager.GetUserAsync(User);
        return View(user);
      }
      catch
      {
        throw new NotImplementedException();
      }
    }

    public async Task<IActionResult> AccessDenied(string returnUrl)
    {
      var user = await userManager.GetUserAsync(User);
      var vm = new AccessDeniedViewModel
      {
        Url = returnUrl,
        UserName = user.UserName,
        Email = user.Email
      };

      return View(vm);
    }
  }
}