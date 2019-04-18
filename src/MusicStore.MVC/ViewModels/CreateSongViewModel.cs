using MusicStore.MVC.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.MVC.ViewModels
{
  public class CreateSongViewModel
  {
    private SongForCreatingDto _dto;

    public SongForCreatingDto Dto
    {
      get { return _dto; }
      set
      {
        _dto = value;
        if (_dto != null)
        {
          _dto.GenresIds = Genres.Where(g => g.IsSelected).Select(s => s.Id);
        }
      }
    }

    public string AlbumName { get; set; }
    public List<CheckBoxItem> Genres { get; set; }
      = new List<CheckBoxItem>();
  }
}
