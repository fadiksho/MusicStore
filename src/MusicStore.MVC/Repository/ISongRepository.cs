using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface ISongRepository
  {
    Task<Song> GetAsync(int songId);
    Task<IEnumerable<Song>> GetAllAsync();
    Task AddAsync(SongForCreatingDto dto);
    Task UpdateAsync(int songId, SongForUpdatingDto dto);
    Task DeleteAsync(int songId);

    Task AssignSongToAlbum(int songId, int? albumId);
  }
}
