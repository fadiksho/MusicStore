using System.Threading.Tasks;
using AutoMapper;
using MusicStore.MVC.Repository;
using MusicStore.MVC.Repository.Data;

namespace MusicStore.MVC.Persistence.Data
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly MusicStoreContext context;

    public UnitOfWork(MusicStoreContext context, IMapper mapper)
    {
      this.context = context;

      this.Songs = new SongRepository(context, mapper);
      this.Albums = new AlbumRepository(context, mapper);
      this.Genres = new GenreRepository(context, mapper);
    }

    public ISongRepository Songs { get; private set; }

    public IAlbumRepository Albums { get; private set; }

    public IGenreRepository Genres { get; private set; }

    public async Task<bool> SaveAsync()
    {
      return await context.SaveChangesAsync() > 0;
    }
  }
}
