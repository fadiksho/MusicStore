using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface IGenreRepository
  {
    Task<IEnumerable<Genre>> GetAllAsync();
    Task AddAsync(GenreForCreatingDto dto);
    Task UpdateAsync(int genreId, GenreForUpdatingDto dto);
    Task DeleteAsync(int genreId);
  }
}
