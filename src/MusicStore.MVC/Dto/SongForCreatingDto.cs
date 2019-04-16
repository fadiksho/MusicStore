using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Dto
{
  public class SongForCreatingDto
  {
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public int? AlbumId { get; set; }

    public IEnumerable<int> GenresIds { get; set; }
      = new List<int>();
  }
}
