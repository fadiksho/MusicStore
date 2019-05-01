using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Persistence.Data
{
  public class DbInitializer
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new MusicStoreContext(
        serviceProvider.GetRequiredService<DbContextOptions<MusicStoreContext>>()))
      {
        context.Database.Migrate();
      }
    }
    public static void InitializeData(IServiceProvider serviceProvider, ILogger<Program> logger, string userId)
    {
      using (var context = new MusicStoreContext(
        serviceProvider.GetRequiredService<DbContextOptions<MusicStoreContext>>()))
      {
        if (!context.Songs.AsNoTracking().Any())
        {
          // creating 3 albums
          var albums = new List<AlbumEntity>();
          for (int i = 0; i < 3; i++)
          {
            albums.Add(new AlbumEntity
            {
              Name = $"Album {i + 1}",
              Description = "description that the album will have just a doemo test",
              OwenerId = userId
            });
          }
          // creating 15 songs
          var songs = new List<SongEntity>();
          for (int i = 0; i < 15; i++)
          {
            var song = new SongEntity
            {
              Name = $"Song {i + 1}",
              Album = albums[i % 3],
              OwenerId = userId
            };
            songs.Add(song);
          };
          // creating 10 genre
          var genres = new List<GenreEntity>();
          for (int i = 0; i < 10; i++)
          {
            genres.Add(new GenreEntity { Name = $"Genre {(char)i + 42}" });
          }
          // linking songs to genres
          var genresToSong = new List<GenreSongEntity>();
          var rand = new Random();
          foreach (var song in songs)
          {
            // add random tags to song
            var randGenrsCount = rand.Next(0, 3);
            for (int i = 0; i <= randGenrsCount - 1; i++)
            {
              genresToSong.Add(new GenreSongEntity
              {
                Song = song,
                Genre = genres[rand.Next(genres.Count)]
              });
            }
          }
          logger.LogInformation("Adding generes to songs..");
          context.AddRange(genresToSong);
          // add song without album and genre
          context.Songs.Add(new SongEntity { Name = "Without Genre and Album" });
          context.SaveChanges();
          logger.LogInformation("Saving Succeseded...");
        }
      }
    }
    public static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string userName, string email, string password)
    {
      var userManager = serviceProvider.GetService<UserManager<User>>();

      var user = await userManager.FindByNameAsync(userName);
      if (user == null)
      {
        user = new User { UserName = userName, Email = email, EmailConfirmed = true };
        await userManager.CreateAsync(user, password);
      }

      return user.Id;
    }

    public static async Task EnsureRole(IServiceProvider serviceProvider,
                                                              string uid, string role)
    {
      var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

      if (roleManager == null)
      {
        throw new Exception("roleManager null");
      }

      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }

      var userManager = serviceProvider.GetService<UserManager<User>>();

      var user = await userManager.FindByIdAsync(uid);
      if (!await userManager.IsInRoleAsync(user, role))
      {
        await userManager.AddToRoleAsync(user, role);
      }
    }
  }
}
