﻿using System.Collections.Generic;

namespace MusicStore.MVC.Entities
{
  public class SongEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public int? AlbumId { get; set; }
    public AlbumEntity Album { get; set; }
    public List<GenreSongEntity> GenreSong { get; set; }
      = new List<GenreSongEntity>();

    public string OwenerId { get; set; }
  }
}
