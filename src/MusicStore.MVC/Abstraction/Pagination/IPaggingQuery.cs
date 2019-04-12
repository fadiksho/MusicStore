using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.MVC.Abstraction.Pagination
{
	public interface IPaggingQuery
	{
		int Page { get; set; }
		int PageSize { get; set; }
	}
}
