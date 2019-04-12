using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;
using System.Linq;

namespace MusicStore.MVC.MappingProfiles
{
  public class GenreProfile : Profile
  {
    public GenreProfile()
    {
      CreateMap<GenreEntity, Genre>();
      CreateMap<GenreForCreatingDto, GenreEntity>();
      CreateMap<GenreForUpdatingDto, GenreEntity>();
    }
  }
}
