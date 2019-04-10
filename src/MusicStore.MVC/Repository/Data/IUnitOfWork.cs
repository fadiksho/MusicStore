using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository.Data
{
  public interface IUnitOfWork
  {
    ISongRepository Songs { get; }
    IAlbumRepository Albums { get; }
    IGenreRepository Genres { get; }

    Task<bool> SaveAsync();
  }
}
