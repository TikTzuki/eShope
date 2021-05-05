using System;
using System.Collections.Generic;
using Filter;
using Baseurl;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
namespace Baseurl
{
    public class urlService : IrulService
    {
     private readonly string _base_url;
     public urlService(string base_url)
     {
         _base_url=base_url;
     }
     public Uri getpageurl(PagedFilter filter,string route)
     {
         var endpointurl= new Uri(string.Concat(_base_url,route));
         var modifield=(QueryHelpers.AddQueryString(endpointurl.ToString(),"pagenumber",filter.pageIndex.ToString()));
         modifield=(QueryHelpers.AddQueryString(modifield,"pagesize",filter.pageSize.ToString()));
         return new Uri(modifield);
     }
    }
}