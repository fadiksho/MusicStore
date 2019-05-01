using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface ISongRepository
  {
    Task<Song> GetAsync(int songId);
    Task<PaggingResult<Song>> GetSongPage(IPaggingQuery query = null);
    Task<SongEntity> AddAsync(SongForCreatingDto dto);
    Task UpdateAsync(SongForUpdatingDto dto);
    Task DeleteAsync(int songId);

    Task AddSongToAlbum(int songId, int? albumId);
  }
}
