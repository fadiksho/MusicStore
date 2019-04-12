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

    public async Task AddAsync(AlbumForCreatingDto dto)
    {
      var albumEntity = mapper.Map<AlbumEntity>(dto);

      await context.Albums.AddAsync(albumEntity);
    }
    public async Task UpdateAsync(int albumId, AlbumForUpdatingDto dto)
    {
      var albumEntity = await context.Albums
        .FirstOrDefaultAsync(a => a.Id == albumId);

      mapper.Map(albumEntity, dto);
    }
    public async Task DeleteAsync(int albumId)
    {
      var albumEntity = await context.Albums
        .FirstOrDefaultAsync(a => a.Id == albumId);

      context.Albums.Remove(albumEntity);
    }
  }
}
