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
      context.Database.EnsureCreated();

      if (context.Songs.Any())
        return;

      // creating 3 albums
      var albums = new List<AlbumEntity>();
      for (int i = 0; i < 3; i++)
      {
        albums.Add(new AlbumEntity { Name = $"Album {i + 1}" });
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
      // saving songs, genres and albums to db
      logger.LogInformation("Adding Songs..");
      context.Songs.AddRange(songs);
      logger.LogInformation("Adding Genres..");
      context.Genres.AddRange(genres);
      logger.LogInformation("Saving..");
      context.SaveChanges();
      logger.LogInformation("Saving Succeseded");
      // linking songs to genres
      var genresToSong = new List<GenreSongEntity>();
      var rand = new Random();
      foreach (var song in songs)
      {
        // add random tags to song
        var randGenrsCount = rand.Next(0, 3);
        for (int i = 0; i < randGenrsCount; i++)
        {
          genresToSong.Add(new GenreSongEntity
          {
            SongId = song.Id,
            GenreId = genres[rand.Next(genres.Count)].Id
          });
        }
      }
      logger.LogInformation("Adding generes to songs..");
      context.AddRange(genresToSong);
      context.SaveChanges();
      logger.LogInformation("Saving Succeseded...");
    }
  }
}
