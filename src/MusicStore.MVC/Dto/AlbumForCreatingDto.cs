using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Dto
{
  public class AlbumForCreatingDto
  {
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
  }
}
