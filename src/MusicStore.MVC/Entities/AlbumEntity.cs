using System.Collections.Generic;

namespace MusicStore.MVC.Entities
{
  public class AlbumEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public List<SongEntity> Songs { get; set; }

    public string OwenerId { get; set; }
  }
}
