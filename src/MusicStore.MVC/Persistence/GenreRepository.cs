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
  public class GenreRepository : IGenreRepository
  {
    private readonly MusicStoreContext context;
    private readonly IMapper mapper;

    public GenreRepository(MusicStoreContext context, IMapper mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
      var genreEntities = await context.Genres
        .AsNoTracking()
        .ToListAsync();

      return mapper.Map<IEnumerable<Genre>>(genreEntities);
    }

    public async Task AddAsync(GenreForCreatingDto dto)
    {
      var genreEntity = mapper.Map<GenreEntity>(dto);

      await context.Genres.AddAsync(genreEntity);
    }
    public async Task UpdateAsync(GenreForUpdatingDto dto)
    {
      var genreEntity = await context.Genres
        .FirstOrDefaultAsync(g => g.Id == dto.Id);

      mapper.Map(dto, genreEntity);
    }
    public async Task DeleteAsync(int genreId)
    {
      var genreEntity = await context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

      context.Genres.Remove(genreEntity);
    }
  }
}
