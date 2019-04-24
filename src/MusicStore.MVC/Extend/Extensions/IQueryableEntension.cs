using MusicStore.MVC.Abstraction.Pagination;
using System.Linq;

namespace MusicStore.MVC.Extend.Extensions
{
  public static class IQueryableEntension
  {
    public static IQueryable<T> ApplayPaging<T>(this IQueryable<T> query, IPaggingQuery pagingParameter)
    {
      return query
        .Skip(pagingParameter.PageSize * (pagingParameter.Page - 1))
        .Take(pagingParameter.PageSize);
    }
  }
}
