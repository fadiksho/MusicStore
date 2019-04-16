using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.MappingProfiles;
using MusicStore.MVC.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicStore.Test.Repository
{
  public class AlbumRepositoryTest
  {
    readonly IMapper _mapper;
    public AlbumRepositoryTest()
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
    public async Task Add_Album_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);

          var album = new AlbumForCreatingDto
          {
            Name = "First Album"
          };

          await unitOfWork.Albums.AddAsync(album);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          Assert.NotNull(context.Albums.First());
        }
      }
    }

    [Fact]
    public async Task Update_Album_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        var albumId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          var album = new AlbumEntity
          {
            Name = "Name",
            Description = "Description"
          };
          context.Albums.Add(album);
          context.SaveChanges();

          albumId = album.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var albumForUpdateDto = new AlbumForUpdatingDto
          {
            Id = albumId,
            Name = "Updated",
            Description = "Updated"
          };
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Albums.UpdateAsync(albumForUpdateDto);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var album = context.Albums.First();
          Assert.Equal("Updated", album.Name);
          Assert.Equal("Updated", album.Description);
        }
      }
    }

    [Fact]
    public async Task Delete_Album_Should_Persiste()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        var albumId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          var album = new AlbumEntity
          {
            Name = "Name",
            Description = "Description"
          };
          context.Albums.Add(album);
          context.SaveChanges();

          albumId = album.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Albums.DeleteAsync(albumId);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var album = context.Albums.FirstOrDefault();
          Assert.Null(album);
        }
      }
    }

    [Fact]
    public async Task Delete_Album_Should_Not_Delete_The_Songs_In_That_Album()
    {
      var albumId = 0;
      using (var factory = new MusicStoreContextFactory())
      {
        using (var context = factory.CreateMusicStoreContext())
        {
          var album = new AlbumEntity
          {
            Name = "Name",
            Description = "Description",
            Songs = new List<SongEntity>
            {
              new SongEntity()
            }
          };
          context.Albums.Add(album);
          context.SaveChanges();

          albumId = album.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Albums.DeleteAsync(albumId);
          await unitOfWork.SaveAsync();
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var song = context.Songs.Include(s => s.Album).First();
          Assert.NotNull(song);
          Assert.Null(song.Album);
        }
      }
    }

    [Fact]
    public async Task Get_Album_Should_Not_Be_Null()
    {
      using (var factory = new MusicStoreContextFactory())
      {
        var albumId = 0;
        using (var context = factory.CreateMusicStoreContext())
        {
          var album = new AlbumEntity
          {
            Name = "Name",
            Description = "Description"
          };
          context.Albums.Add(album);
          context.SaveChanges();

          albumId = album.Id;
        }
        using (var context = factory.CreateMusicStoreContext())
        {
          var unitOfWork = new UnitOfWork(context, _mapper);
          var album = await unitOfWork.Albums.GetAsync(albumId);
          Assert.NotNull(album);
        }
      }
    }
  }
}
