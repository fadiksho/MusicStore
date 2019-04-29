using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MusicStore.MVC.Models;
using MusicStore.MVC.ViewModels;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MusicStore.MVC.Controllers
{
  [Authorize]
  public class AccountsController : Controller
  {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IEmailSender emailSender;
    
    public AccountsController(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      IEmailSender emailSender)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.emailSender = emailSender;
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
      if (ModelState.IsValid)
      {
        var user = new User
        {
          UserName = vm.UserName,
          Email = vm.Email
        };
        var result = await userManager.CreateAsync(
          user,
          vm.Password
        );

        if (result.Succeeded)
        {
          // send the confirmation to link to user email
          await sendConfirmationLinkAsync(user);

          // await signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToAction("Index", "Songs");
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      // If we got this far, something failed, redisplay form
      return View();
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
          // check if user Email is confirmed
          if (!user.EmailConfirmed)
          {
            var emailConfirmationVM = new EmailConfirmationViewModel
            {
              Email = user.Email,
              UserName = user.UserName
            };
            return View("EmailConfirmationResend", emailConfirmationVM);
          }
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

    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
      if (userId == null || code == null)
      {
        return RedirectToAction("Index", "Songs");
      }

      var user = await userManager.FindByIdAsync(userId);
      if (user == null)
      {
        return NotFound($"Unable to load user with ID '{userId}'.");
      }

      var result = await userManager.ConfirmEmailAsync(user, code);
      if (!result.Succeeded)
      {
        throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
      }

      await signInManager.SignInAsync(user, isPersistent: false);

      return RedirectToAction("Index", "Songs");
    }

    [AllowAnonymous]
    public async Task<IActionResult> EmailConfirmationResend()
    {
      var user = await userManager.FindByEmailAsync("fadiksho@gmail.com");
      return View(user);
    }
    [AllowAnonymous]
    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EmailConfirmationResend(EmailConfirmationViewModel vm)
    {
      var user = await userManager.FindByEmailAsync(vm.Email);
      if (user != null)
      {
        if (!user.EmailConfirmed)
        {
          await sendConfirmationLinkAsync(user);
        }

        vm.IsConfirmationLinkSent = true;
        return View(vm);
      }
      vm.Message = $"The Email {vm.Email} is not exist.";
      return View(vm);
    }
    private async Task sendConfirmationLinkAsync(User user)
    {
      var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
      var callbackUrl = Url.Action("ConfirmEmail", "Accounts",
       new { userId = user.Id, code = code },
       Request.Scheme);

      await emailSender.SendEmailAsync(user.Email, "Confirm your email",
         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
    }
  }
}