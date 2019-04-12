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
  public class GenreRepositoryTest
  {
    readonly IMapper _mapper;
    public GenreRepositoryTest()
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
    public async Task Add_Genre_Should_Persiste()
    {
      var connection = new SqliteConnection("DataSource=:memory:");
      connection.Open();
      try
      {
        var options = new DbContextOptionsBuilder<MusicStoreContext>()
                .UseSqlite(connection)
                .Options;

        using (var context = new MusicStoreContext(options))
        {
          context.Database.EnsureCreated();
          var unitOfWork = new UnitOfWork(context, _mapper);

          var genre = new GenreForCreatingDto
          {
            Name = "Genre"
          };

          await unitOfWork.Genres.AddAsync(genre);
          await unitOfWork.SaveAsync();
        }
        using (var context = new MusicStoreContext(options))
        {
          Assert.NotNull(context.Genres.First());
        }
      }
      finally
      {
        connection.Close();
      }
    }

    [Fact]
    public async Task Update_Genre_Should_Persiste()
    {
      var connection = new SqliteConnection("DataSource=:memory:");
      connection.Open();
      try
      {
        var options = new DbContextOptionsBuilder<MusicStoreContext>()
                .UseSqlite(connection)
                .Options;
        int genreId = 0;
        using (var context = new MusicStoreContext(options))
        {
          context.Database.EnsureCreated();

          var genre = new GenreEntity
          {
            Name = "Name"
          };
          context.Genres.Add(genre);
          context.SaveChanges();

          genreId = genre.Id;
        }
        using (var context = new MusicStoreContext(options))
        {
          var genreForUpdateDto = new GenreForUpdatingDto
          {
            Id = genreId,
            Name = "Updated"
          };
          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Genres.UpdateAsync(genreForUpdateDto);
          await unitOfWork.SaveAsync();
        }
        using (var context = new MusicStoreContext(options))
        {
          var genre = context.Genres.First();
          Assert.Equal("Updated", genre.Name);
        }
      }
      finally
      {
        connection.Close();
      }
    }

    [Fact]
    public async Task Delete_Album_Should_Persiste()
    {
      var connection = new SqliteConnection("DataSource=:memory:");
      connection.Open();
      try
      {
        var options = new DbContextOptionsBuilder<MusicStoreContext>()
                .UseSqlite(connection)
                .Options;
        int genreId = 0;
        using (var context = new MusicStoreContext(options))
        {
          context.Database.EnsureCreated();

          var genre = new GenreEntity
          {
            Name = "Name"
          };
          context.Genres.Add(genre);
          context.SaveChanges();

          genreId = genre.Id;
        }
        using (var context = new MusicStoreContext(options))
        {

          var unitOfWork = new UnitOfWork(context, _mapper);
          await unitOfWork.Genres.DeleteAsync(genreId);
          await unitOfWork.SaveAsync();
        }
        using (var context = new MusicStoreContext(options))
        {
          var genre = context.Genres.FirstOrDefault();
          Assert.Null(genre);
        }
      }
      finally
      {
        connection.Close();
      }
    }
  }
}
