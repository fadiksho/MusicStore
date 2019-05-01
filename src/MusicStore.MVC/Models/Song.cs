using System.Collections.Generic;

namespace MusicStore.MVC.Models
{
  public class Song
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public Album Album { get; set; }
    public List<Genre> Genres { get; set; }
      = new List<Genre>();

    public string OwenerId { get; set; }
  }
}
