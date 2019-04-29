using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.MVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Persistence.Data
{
  public class DbInitializer
  {
    public static void Initialize(MusicStoreContext context, ILogger<Program> logger)
    {
      context.Database.Migrate();

      if (!context.Songs.Any())
      {
        // creating 3 albums
        var albums = new List<AlbumEntity>();
        for (int i = 0; i < 3; i++)
        {
          albums.Add(new AlbumEntity
          {
            Name = $"Album {i + 1}",
            Description = "description that the album will have just a doemo test"
          });
        }
        // creating 15 songs
        var songs = new List<SongEntity>();
        for (int i = 0; i < 15; i++)
        {
          var song = new SongEntity
          {
            Name = $"Song {i + 1}",
            Album = albums[i % 3]
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

      if (!context.Roles.Any())
      {
        var adminRole = new IdentityRole
        {
          Name = "Administrator",
          NormalizedName = "Administrator".ToUpper()
        };
        context.Roles.Add(adminRole);
        context.SaveChanges();
      }
    }
  }
}
