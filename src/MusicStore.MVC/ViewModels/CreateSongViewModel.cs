﻿using MusicStore.MVC.Dto;
using System.Collections.Generic;

namespace MusicStore.MVC.ViewModels
{
  public class CreateSongViewModel
  {
    public SongForCreatingDto Dto { get; set; }

    public string AlbumName { get; set; }
    public List<CheckBoxItem> Genres { get; set; }
      = new List<CheckBoxItem>();
  }
}
