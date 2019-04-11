using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
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

    public Task<Album> GetAsync(int albumId)
    {
      throw new System.NotImplementedException();
    }
    public Task<IEnumerable<Album>> GetAllAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task AddAsync(AlbumForCreatingDto dto)
    {
      throw new System.NotImplementedException();
    }
    public Task UpdateAsync(int albumId, AlbumForUpdatingDto dto)
    {
      throw new System.NotImplementedException();
    }
    public Task DeleteAsync(int albumId)
    {
      throw new System.NotImplementedException();
    }
  }
}
