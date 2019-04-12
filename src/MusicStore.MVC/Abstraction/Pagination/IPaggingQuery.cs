namespace MusicStore.MVC.Abstraction.Pagination
{
  public interface IPaggingQuery
	{
		int Page { get; set; }
		int PageSize { get; set; }
	}
}
