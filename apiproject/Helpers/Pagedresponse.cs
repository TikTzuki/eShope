using System;
using System.Collections.Generic;
using Filter;
using Baseurl;
namespace Helpers
{
  public class Pagedresponse
  {
    public static PagedList<IEnumerable<T>> createpagedresponse<T>(IEnumerable<T> data, PagedFilter filter, int totalcount, IrulService uriservice, string route)
    {
      var res = new PagedList<IEnumerable<T>>(data, filter.pageIndex, filter.pageSize);
      var totalpage = (double)totalcount / (double)filter.pageSize;
      int totalpage1 = Convert.ToInt32(Math.Ceiling(totalpage));
      res.nextpage = filter.pageIndex >= 1 && filter.pageIndex < totalpage ? uriservice.getpageurl(new PagedFilter(filter.pageIndex + 1, filter.pageSize), route) : null;
      res.prevpage = filter.pageIndex - 1 >= 1 && filter.pageIndex <= totalpage ? uriservice.getpageurl(new PagedFilter(filter.pageIndex - 1, filter.pageSize), route) : null;
      res.firstpage = uriservice.getpageurl(new PagedFilter(0, filter.pageSize), route);
      res.lastpage = uriservice.getpageurl(new PagedFilter(totalpage1, filter.pageSize), route);
      res.totalpage = totalpage1;
      res.count = totalcount;
      return res;
    }
  }
}