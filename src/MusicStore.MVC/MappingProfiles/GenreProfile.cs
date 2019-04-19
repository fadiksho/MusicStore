using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;
using MusicStore.MVC.ViewModels;

namespace MusicStore.MVC.MappingProfiles
{
  public class GenreProfile : Profile
  {
    public GenreProfile()
    {
      CreateMap<GenreEntity, Genre>();
      CreateMap<GenreForCreatingDto, GenreEntity>();
      CreateMap<GenreForUpdatingDto, GenreEntity>();
      CreateMap<Genre, CheckBoxItem>();
    }
  }
}
