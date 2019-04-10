using AutoMapper;
using MusicStore.MVC.Persistence.Data;
using MusicStore.MVC.Repository;

namespace MusicStore.MVC.Persistence
{
  public class GenreRepository : IGenreRepository
  {
    private readonly MusicStoreContext context;
    private readonly IMapper mapper;

    public GenreRepository(MusicStoreContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }
  }
}
