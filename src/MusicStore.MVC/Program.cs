using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Authorization;
using MusicStore.MVC.Persistence.Data;

namespace MusicStore.MVC
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
          DbInitializer.Initialize(services);
          var userId = DbInitializer.EnsureUser(services, "admin", "fadiksho@gmail.com", "Password!23").Result;
          DbInitializer.InitializeData(services, logger, userId);
          DbInitializer.EnsureRole(services, userId, AutherazationConstants.AdministratorsRole).Wait();
        }
        catch (Exception ex)
        {
          logger.LogError(ex, "An error occurred while seeding the database.");
        }
      }

      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
