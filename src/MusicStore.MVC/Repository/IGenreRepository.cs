using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface IGenreRepository
  {
    Task<Genre> GetAsync(int id);
    Task<IEnumerable<Genre>> GetAllAsync();
    Task AddAsync(GenreForCreatingDto dto);
    Task UpdateAsync(GenreForUpdatingDto dto);
    Task DeleteAsync(int genreId);
  }
}
