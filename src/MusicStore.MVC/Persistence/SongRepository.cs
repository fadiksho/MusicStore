using AutoMapper;
using MusicStore.MVC.Persistence.Data;
using MusicStore.MVC.Repository;

namespace MusicStore.MVC.Persistence
{
  public class SongRepository : ISongRepository
  {
    private readonly MusicStoreContext context;
    private readonly IMapper mapper;

    public SongRepository(MusicStoreContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }
  }
}
