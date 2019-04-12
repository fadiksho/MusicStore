using System.Collections.Generic;

namespace MusicStore.MVC.Abstraction.Pagination
{
	public class PaggingResult<T> : PaggingData
	{
		public IEnumerable<T> TResult { get; set; }
	}
}
