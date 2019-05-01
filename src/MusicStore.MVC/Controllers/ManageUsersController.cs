using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Models;
using MusicStore.MVC.ViewModels;

namespace MusicStore.MVC.Controllers
{
  [Authorize(Roles = "Administrator")]
  public class ManageUsersController : Controller
  {
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public ManageUsersController(
      UserManager<User> userManager,
      RoleManager<IdentityRole> roleManager)
    {
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
      var users = await userManager.Users.ToListAsync();
      return View(users);
    }

    public async Task<IActionResult> Edit(string id)
    {
      var user = await userManager.FindByIdAsync(id);

      if (user == null)
      {
        // ToDo: Implement notfound page
        return View("NotFound");
      }
      var vm = new EditUserViewModel
      {
        UserId = user.Id,
        Email = user.Email,
        UserName = user.UserName
      };
      var roles = await roleManager.Roles.ToListAsync();
      var userRoles = (await userManager.GetRolesAsync(user));
      foreach (var role in roles)
      {
        var item = new CheckBoxItem();
        item.Name = role.NormalizedName;
        if (userRoles.Any(c => c == role.Name))
          item.IsSelected = true;

        vm.UserRoles.Add(item);
      }

      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel vm)
    {
      if (!ModelState.IsValid)
      {
        return View(vm);
      }
      var user = await userManager.FindByIdAsync(vm.UserId);
      if (user == null)
      {
        vm.Message = "Update user faild";
        return View(vm);
      }
      var addedRoles = vm.UserRoles.Where(c => c.IsSelected == true).Select(c => c.Name).ToList();
      var removededRoles = vm.UserRoles.Where(c => c.IsSelected == false).Select(c => c.Name).ToList();
      if (addedRoles.Count > 0)
        await userManager.AddToRolesAsync(user, addedRoles);

      if (removededRoles.Count > 0)
      {
        // ToDo: Check if we have another admin before removing the role from users
        // the system should have at least on admin
        await userManager.RemoveFromRolesAsync(user, removededRoles);
      }
        
      vm.Message = "Update Succeded";
      return View(vm);
    }
  }
}