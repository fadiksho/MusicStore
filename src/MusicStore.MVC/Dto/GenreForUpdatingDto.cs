using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Dto
{
  public class GenreForUpdatingDto
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
  }
}
