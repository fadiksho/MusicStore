using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface ISongRepository
  {
    Task<Song> GetAsync(int songId);
    Task<PaggingResult<Song>> GetSongPage(IPaggingQuery query = null);
    Task AddAsync(SongForCreatingDto dto);
    Task UpdateAsync(SongForUpdatingDto dto);
    Task DeleteAsync(int songId);

    Task AddSongToAlbum(int songId, int? albumId);
    Task SetGenresToSongAsync(int songId, IEnumerable<int> genresId);
  }
}
