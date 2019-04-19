using MusicStore.MVC.Dto;
using System.Collections.Generic;

namespace MusicStore.MVC.ViewModels
{
  public class EditSongViewModel
  {
    public SongForUpdatingDto Dto { get; set; }

    public string AlbumName { get; set; }
    public List<CheckBoxItem> Genres { get; set; }
    public string Message { get; set; }
  }
}
