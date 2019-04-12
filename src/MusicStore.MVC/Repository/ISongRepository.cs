using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface ISongRepository
  {
    Task<Song> GetAsync(int songId);
    Task<PaggingResult<Song>> GetSongPage(IPaggingQuery query);
    Task AddAsync(SongForCreatingDto dto);
    Task UpdateAsync(int songId, SongForUpdatingDto dto);
    Task DeleteAsync(int songId);

    Task AssignSongToAlbum(int songId, int? albumId);
  }
}
