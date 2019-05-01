using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
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

    public async Task<Album> GetAsync(int albumId)
    {
      var albumEntity = await context.Albums
        .Include(a => a.Songs)
          .ThenInclude(a => a.GenreSong)
            .ThenInclude(a => a.Genre)
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.Id == albumId);

      return mapper.Map<Album>(albumEntity);
    }
    public async Task<IEnumerable<Album>> GetAllAsync()
    {
      var albumEntities = await context.Albums
        .AsNoTracking()
        .ToListAsync();

      return mapper.Map<IEnumerable<Album>>(albumEntities);
    }

    public async Task<AlbumEntity> AddAsync(AlbumForCreatingDto dto)
    {
      var albumEntity = mapper.Map<AlbumEntity>(dto);

      await context.Albums.AddAsync(albumEntity);

      return albumEntity;
    }
    public async Task UpdateAsync(AlbumForUpdatingDto dto)
    {
      var albumEntity = await context.Albums
        .FirstOrDefaultAsync(a => a.Id == dto.Id);

      mapper.Map(dto, albumEntity);
    }
    public async Task DeleteAsync(int albumId)
    {
      var albumEntity = await context.Albums
        .FirstOrDefaultAsync(a => a.Id == albumId);

      context.Albums.Remove(albumEntity);
    }

    public async Task<bool> Exist(int? albumId)
    {
      if (albumId == null)
        return false;

      var album = await context.Albums
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.Id == albumId);

      return album != null;
    }
  }
}
