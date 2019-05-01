using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.MVC.Repository
{
  public interface IAlbumRepository
  {
    Task<Album> GetAsync(int albumId);
    Task<IEnumerable<Album>> GetAllAsync();
    Task<AlbumEntity> AddAsync(AlbumForCreatingDto dto);
    Task UpdateAsync(AlbumForUpdatingDto dto);
    Task DeleteAsync(int albumId);

    Task<bool> Exist(int? albumId);
  }
}
