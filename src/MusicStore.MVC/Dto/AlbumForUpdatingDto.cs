﻿using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Dto
{
  public class AlbumForUpdatingDto
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
  }
}
