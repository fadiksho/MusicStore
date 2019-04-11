using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
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

    public Task<Genre> GetAsync(int genreId)
    {
      throw new System.NotImplementedException();
    }
    public Task<IEnumerable<Genre>> GetAllAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task AddAsync(GenreForCreatingDto dto)
    {
      throw new System.NotImplementedException();
    }
    public Task UpdateAsync(int genreId, GenreForUpdatingDto dto)
    {
      throw new System.NotImplementedException();
    }
    public Task DeleteAsync(int genreId)
    {
      throw new System.NotImplementedException();
    }
  }
}
