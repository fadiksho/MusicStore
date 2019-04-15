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
      CreateMap<SongForCreatingDto, SongEntity>();
      CreateMap<SongForUpdatingDto, SongEntity>();
    }
  }
}
