using System.Collections.Generic;

namespace MusicStore.MVC.Models
{
  public class Album
  {
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public List<Song> Songs { get; set; }
  }
}
