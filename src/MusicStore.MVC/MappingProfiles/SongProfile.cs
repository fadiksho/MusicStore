using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;
using System.Linq;

namespace MusicStore.MVC.MappingProfiles
{
  public class SongProfile : Profile
  {
    public SongProfile()
    {
      CreateMap<SongEntity, Song>()
        .ForMember(model => model.Genres, opt => opt.MapFrom(x => x.GenreSong.Select(y => y.Genre)));
      CreateMap<SongForCreatingDto, SongEntity>()
        .ForMember(e => e.GenreSong, opt => 
          opt.MapFrom(x => x.GenresIds.Select(y => new GenreSongEntity { GenreId = y })));
      CreateMap<SongForUpdatingDto, SongEntity>()
        .ForMember(e => e.GenreSong, opt =>
          opt.MapFrom(x => x.GenresIds.Select(y => new GenreSongEntity { GenreId = y })));
      CreateMap<Song, SongForUpdatingDto>()
        .ForMember(e => e.GenresIds, opt =>
          opt.MapFrom(x => x.Genres.Select(y => y.Id)));
    }
  }
}
