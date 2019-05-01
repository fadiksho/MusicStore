using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using MusicStore.MVC.Models;

namespace MusicStore.MVC.Authorization
{
  public class OwenerResourseHandler
    : AuthorizationHandler<OperationAuthorizationRequirement, string>
  {
    private readonly UserManager<User> userManager;

    public OwenerResourseHandler(UserManager<User> userManager)
    {
      this.userManager = userManager;
    }
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      OperationAuthorizationRequirement requirement, 
      string resource)
    {
      if (context.User == null || resource == null)
      {
        return Task.CompletedTask;
      }

      // If we're not asking for CRUD permission, return.
      if (requirement.Name != AutherazationConstants.OwenResourse)
      {
        return Task.CompletedTask;
      }

      var userId = userManager.GetUserId(context.User);
      if (resource == userId)
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
