namespace MusicStore.MVC.Abstraction.Pagination
{
  public class PaggingQuery : IPaggingQuery
  {
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
  }
}
