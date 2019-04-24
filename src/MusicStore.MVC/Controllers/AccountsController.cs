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
  public class AccountsController : Controller
  {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AccountsController(
      UserManager<User> userManager,
      SignInManager<User> signInManager)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }
    [AllowAnonymous]
    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return View(vm);
        }
       // check if Username is available
       var isUsernameAvailable = (await userManager.FindByNameAsync(vm.UserName) == null);
        if (!isUsernameAvailable)
        {
          vm.Message = $"Username '{vm.UserName}' is already taken.";
          return View(vm);
        }
        // check if Email is available
        var isEmailAvailable = (await userManager.FindByEmailAsync(vm.UserName) == null);
        if (!isEmailAvailable)
        {
          vm.Message = $"Email '{vm.Email}' is already taken.";
          return View(vm);
        }

        var result = await userManager.CreateAsync(
          new User
          {
            UserName = vm.UserName,
            Email = vm.Email
          },
          vm.Password
        );

        if (result.Succeeded)
        {
          return View("LogIn", new LogInViewModel
          {
            Email = vm.Email,
          });
        }
        vm.Message = "An error happened while creating your account try again.";
        return View(vm);
      }
      catch
      {
        throw new NotImplementedException();
      }
    }

    [AllowAnonymous]
    public IActionResult LogIn()
    {
      return View();
    }
    [AllowAnonymous]
    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> LogIn(LogInViewModel vm, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var user = await userManager.FindByEmailAsync(vm.Email);
        if (user != null)
        {
          var result = await signInManager.PasswordSignInAsync(user.UserName,
            vm.Password, vm.RememberMe, lockoutOnFailure: false);
          if (result.Succeeded)
          {
            if (Url.IsLocalUrl(returnUrl))
            {
              return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Songs");
          }
          else
          {
            vm.Message = "Invalid login attempt.";
            return View(vm);
          }
        }
        else
        {
          vm.Message = "This account is not exist.";
          return View(vm);
        }
      }

      // If we got this far, something failed, reset the loging form
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      try
      {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Songs");
      }
      catch (Exception ex)
      {
        throw new NotImplementedException();
      }
    }
  }
}