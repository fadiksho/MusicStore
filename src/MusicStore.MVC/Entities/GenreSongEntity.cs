namespace MusicStore.MVC.Entities
{
  public class GenreSongEntity
  {
    public int GenreId { get; set; }
    public GenreEntity Genre { get; set; }

    public int SongId { get; set; }
    public SongEntity Song { get; set; }
  }
}
