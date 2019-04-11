using System.ComponentModel.DataAnnotations;

namespace MusicStore.MVC.Dto
{
  public class GenreForCreatingDto
  {
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
  }
}
