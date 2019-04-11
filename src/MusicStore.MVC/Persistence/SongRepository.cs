using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
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

    public Task<Song> GetAsync(int songId)
    {
      throw new System.NotImplementedException();
    }
    public Task<IEnumerable<Song>> GetAllAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task AddAsync(SongForCreatingDto dto)
    {
      throw new System.NotImplementedException();
    }
    public Task UpdateAsync(int songId, SongForUpdatingDto dto)
    {
      throw new System.NotImplementedException();
    }
    public Task DeleteAsync(int songId)
    {
      throw new System.NotImplementedException();
    }

    public Task AssignSongToAlbum(int songId, int? albumId)
    {
      throw new System.NotImplementedException();
    }
  }
}
