using System.Collections.Generic;

namespace MusicStore.MVC.Entities
{
  public class GenreEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public List<GenreSongEntity> GenreSong { get; set; }
      = new List<GenreSongEntity>();
  }
}
