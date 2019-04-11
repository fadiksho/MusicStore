using System.Collections.Generic;

namespace MusicStore.MVC.Models
{
  public class Genre
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Song> Songs { get; set; }
      = new List<Song>();
  }
}
