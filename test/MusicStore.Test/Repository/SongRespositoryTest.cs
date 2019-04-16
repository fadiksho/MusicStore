using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.MappingProfiles;
using MusicStore.MVC.Models;
using MusicStore.MVC.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicStore.Test.Repository
{
  public class SongRespositoryTest
  {
    readonly IMapper _mapper;
    public SongRespositoryTest()
    {
      var mapperConfig = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<AlbumProfile>();
        cfg.AddProfile<SongProfile>();
        cfg.AddProfile<GenreProfile>();
      });
      this._mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Adding_Song_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);

          var song = new SongForCreatingDto
          {
            Name = "First Song"
          };

          await unitOfWork.Songs.AddAsync(song);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          Assert.NotNull(context.Songs.First());
        }
      }
    }

    [Fact]
    public async Task Adding_Song_With_Album_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        using (var context = factory.CreateMusicStoreContext())
        {
          context.Database.EnsureCreated();
          var unitOfWork = new UnitOfWork(context, _mapper);

          var album = new AlbumEntity();
          context.Albums.Add(album);
          context.SaveChanges();

          var song = new SongForCreatingDto
          {
            Name = "First Song",
            AlbumId = album.Id
          };

          await unitOfWork.Songs.AddAsync(song);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = context.Songs.Include(a => a.Album).First();
          Assert.NotNull(song.Album);
        }
      }
    }

    [Fact]
    public async Task Set_Genres_To_Song_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        var genresId = new List<int>();
        var songId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          context.Database.EnsureCreated();
          var genres = new List<GenreEntity>
          {
            new GenreEntity
            {
              Name = "Genre 1"
            },
            new GenreEntity
            {
              Name = "Genre 2"
            }
          };
          context.Genres.AddRange(genres);

          var song = new SongEntity()
          {
            GenreSong = new List<GenreSongEntity>()
            {
              new GenreSongEntity
              {
                Genre = new GenreEntity()
              }
            }
          };
          context.Songs.Add(song);
          context.SaveChanges();

          genresId.Add(genres[0].Id);
          genresId.Add(genres[1].Id);
          songId = song.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Songs.SetGenresToSongAsync(songId, genresId);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = context.Songs.Include(c => c.GenreSong).First();
          Assert.Equal(2, song.GenreSong.Count);
        }
      }
    }

    [Fact]
    public async Task Adding_Song_With_Invalid_AlbumId_Should_NOT_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);

          var song = new SongForCreatingDto
          {
            Name = "First Song",
            AlbumId = 1
          };

          try
          {
            await unitOfWork.Songs.AddAsync(song);
            await unitOfWork.SaveAsync();
          }
          catch { }
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          Assert.Null(context.Songs.FirstOrDefault());
        }
      }
    }

    [Fact]
    public async Task Add_Existing_Song_ToExisting_Album_Should_Persisite()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        int songId = 0;
        int albumId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = new SongEntity();
          var album = new AlbumEntity();

          context.Songs.Add(song);
          context.Albums.Add(album);
          context.SaveChanges();

          songId = song.Id;
          albumId = album.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Songs.AddSongToAlbum(songId, albumId);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = context.Songs.Include(s => s.Album).First();
          Assert.NotNull(song.Album);
        }
      }
    }

    [Fact]
    public async Task Update_Song_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        int songId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = new SongEntity
          {
            Name = "First Song",
            Album = new AlbumEntity()
          };
          context.Songs.Add(song);
          context.SaveChanges();

          songId = song.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var songForUpdateDto = new SongForUpdatingDto
          {
            Id = songId,
            Name = "Updated"
          };
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Songs.UpdateAsync(songForUpdateDto);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = context.Songs.First();
          Assert.Equal("Updated", song.Name);
          Assert.Null(song.AlbumId);
        }
      }
    }

    [Fact]
    public async Task Delete_Song_Should_Persiste_Without_Deleting_The_Associated_Album()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        int songId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          context.Database.EnsureCreated();

          var song = new SongEntity
          {
            Name = "First Song",
            Album = new AlbumEntity()
          };
          context.Songs.Add(song);
          context.SaveChanges();

          songId = song.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Songs.DeleteAsync(songId);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = context.Songs.FirstOrDefault();
          var album = context.Albums.First();
          Assert.Null(song);
          Assert.NotNull(album);
        }
      }
    }

    [Fact]
    public async Task Get_Song_Should_Include_Genres_And_Album()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        int songId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          context.Database.EnsureCreated();

          var song = new SongEntity
          {
            Name = "First Song",
            Album = new AlbumEntity()
          };
          var genreSongs = new List<GenreSongEntity>
          {
            new GenreSongEntity
            {
              Genre = new GenreEntity
              {
                Name = "Romantic"
              },
              Song = song
            },
            new GenreSongEntity
            {
              Genre = new GenreEntity
              {
                Name = "Classic"
              },
              Song = song
            }
          };
          context.AddRange(genreSongs);
          context.SaveChanges();

          songId = song.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);

          var song = await unitOfWork.Songs.GetAsync(songId);
          Assert.NotNull(song.Album);
          Assert.Equal(2, song.Genres.Count);
        }
      }
    }

    [Fact]
    public async Task Get_Song_Page_Should_Not_Include_Album_Only_Include_Genere()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        using (var context = factory.CreateMusicStoreContext())
        {
          var genres = new List<GenreEntity>
          {
            new GenreEntity{ Name = "1" },
            new GenreEntity{ Name = "2" },
          };
          var songs = new List<SongEntity>
          {
            new SongEntity{ Album = new AlbumEntity() },
            new SongEntity{ Album = new AlbumEntity() }
          };

          var genreSongs = new List<GenreSongEntity>
          {
            new GenreSongEntity
            {
              Genre = genres[0],
              Song = songs[0]
            },
            new GenreSongEntity
            {
              Genre = genres[1],
              Song = songs[0]
            },
            new GenreSongEntity
            {
              Genre = genres[0],
              Song = songs[1]
            },
            new GenreSongEntity
            {
              Genre = genres[1],
              Song = songs[1]
            },
          };
          context.AddRange(genreSongs);
          context.SaveChanges();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);

          var songPage = await unitOfWork.Songs.GetSongPage();
          foreach (var song in songPage.TResult)
          {
            Assert.Null(song.Album);
            Assert.Equal(2, song.Genres.Count);
          }
        }
      }
    }
  }
}
