﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.MVC.Models;
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
      catch (Exception ex)
      {
        throw new NotImplementedException();
      }
    }
  }
}