using AutoMapper;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Entities;
using MusicStore.MVC.Models;

namespace MusicStore.MVC.MappingProfiles
{
  public class AlbumProfile : Profile
  {
    public AlbumProfile()
    {
      CreateMap<AlbumEntity, Album>();
      CreateMap<AlbumForCreatingDto, AlbumEntity>();
      CreateMap<AlbumForUpdatingDto, AlbumEntity>();
    }
  }
}
