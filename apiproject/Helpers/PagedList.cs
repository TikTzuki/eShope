using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Helpers
{
    public class PagedList<T>:Response<T>
    {
        public int pageIndex{get;set;}
        public int pageSize{get;set;}
        public int totalpage{get;set;}
        public Uri firstpage{get;set;}
        public Uri lastpage{get;set;}
        public Uri nextpage{get;set;}
        public Uri prevpage{get;set;}
        public int count{get;set;}
    
    public PagedList(T data,int pageIndex,int pagesize)
    {
        this.pageIndex=pageIndex;
        this.pageSize=pagesize;
        this.Data=data;
    }
    }
}