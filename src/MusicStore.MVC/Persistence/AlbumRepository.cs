using AutoMapper;
using MusicStore.MVC.Persistence.Data;
using MusicStore.MVC.Repository;

namespace MusicStore.MVC.Persistence
{
  public class AlbumRepository : IAlbumRepository
  {
    private readonly MusicStoreContext context;
    private readonly IMapper mapper;

    public AlbumRepository(MusicStoreContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }
  }
}
