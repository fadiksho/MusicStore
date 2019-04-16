using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Persistence.Data;
using System;
using System.Data.Common;

namespace MusicStore.Test.Repository
{
  public class MusicStoreContextFactory : IDisposable
  {
    private DbConnection _connection;

    private DbContextOptions<MusicStoreContext> CreateOptions()
    {
      return new DbContextOptionsBuilder<MusicStoreContext>()
          .UseSqlite(_connection).Options;
    }

    public MusicStoreContext CreateMusicStoreContext()
    {
      if (_connection == null)
      {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = CreateOptions();
        using (var context = new MusicStoreContext(options))
        {
          context.Database.EnsureCreated();
        }
      }

      return new MusicStoreContext(CreateOptions());
    }

    public void Dispose()
    {
      if (_connection != null)
      {
        _connection.Dispose();
        _connection = null;
      }
    }
  }
}
