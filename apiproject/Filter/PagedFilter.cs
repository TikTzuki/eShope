using System;
using Helpers;
using System.Collections.Generic;
namespace Filter
{
  public class PagedFilter
  {
    public int pageIndex { get; set; }
    public int pageSize { get; set; }
    public PagedFilter(int pageIndex, int pagesize)
    {
      this.pageIndex = pageIndex < 0 ? 0 : pageIndex;
      this.pageSize = pagesize < 1 ? 1 : pagesize;
    }
    public PagedFilter()
    {
      this.pageIndex = 0;
      this.pageSize = 10;
    }

  }
}