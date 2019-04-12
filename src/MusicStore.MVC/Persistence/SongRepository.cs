using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicStore.MVC.Abstraction.Pagination;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Extensions;
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

    public async Task<Song> GetAsync(int songId)
    {
      var songEntity = await context.Songs
        .Include(s => s.Album)
        .Include(s => s.Genres)
          .ThenInclude(s => s.Genre)
        .FirstOrDefaultAsync(s => s.Id == songId);

      return mapper.Map<Song>(songEntity);
    }
    public async Task<PaggingResult<Song>> GetSongPage(IPaggingQuery query = null)
    {
      if (query == null) query = new PaggingQuery();

      var songsQuery = context.Songs.Include(s => s.Genres)
        .Include(s => s.Genres)
          .ThenInclude(s => s.Genre)
        .AsTracking()
        .AsQueryable();

      var totalItems = songsQuery.Count();

      var songsEntities = await songsQuery.ApplayPaging(query).ToArrayAsync();

      var paggingResult = new PaggingResult<Song>
      {
        CurrentPage = query.Page,
        PageSize = query.PageSize,
        TotalItems = totalItems,
        TResult = mapper.Map<IEnumerable<Song>>(songsEntities)
      };

      return paggingResult;
    }

    public async Task AddAsync(SongForCreatingDto dto)
    {
      var songEntity = mapper.Map<SongEntity>(dto);

      await context.AddAsync(songEntity);
    }
    public async Task UpdateAsync(SongForUpdatingDto dto)
    {
      var songEntity = await context.Songs
        .FirstOrDefaultAsync(s => s.Id == dto.Id);

      mapper.Map(dto, songEntity);
    }
    public async Task DeleteAsync(int songId)
    {
      var songEntity = await context.Songs
        .FirstOrDefaultAsync(s => s.Id == songId);

      context.Songs.Remove(songEntity);
    }

    public async Task AddSongToAlbum(int songId, int? albumId)
    {
      var songEntity = await context.Songs
        .FirstOrDefaultAsync(s => s.Id == songId);

      songEntity.AlbumId = albumId;
    }
  }
}
