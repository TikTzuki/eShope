using System;
using System.Collections.Generic;
using Filter;
namespace Baseurl
{
    public interface IrulService
    {
        public Uri getpageurl(PagedFilter filter,string route);
    }
}