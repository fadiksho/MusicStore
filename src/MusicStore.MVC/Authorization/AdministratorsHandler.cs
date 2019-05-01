using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace MusicStore.MVC.Authorization
{
  public class AdministratorsHandler
    : AuthorizationHandler<OperationAuthorizationRequirement>
  {
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      OperationAuthorizationRequirement requirement)
    {
      if (context.User == null)
      {
        return Task.CompletedTask;
      }

      // Administrators can do anything.
      if (context.User.IsInRole(AutherazationConstants.AdministratorsRole))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
