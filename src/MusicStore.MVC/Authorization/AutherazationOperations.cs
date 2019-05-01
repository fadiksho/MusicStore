using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace MusicStore.MVC.Authorization
{
  public class AutherazationOperations
  {
    public static OperationAuthorizationRequirement OwenResourse =
          new OperationAuthorizationRequirement { Name = AutherazationConstants.OwenResourse };
    
    public static OperationAuthorizationRequirement Administrator =
      new OperationAuthorizationRequirement { Name = AutherazationConstants.AdministratorsRole }; 
  }
}
