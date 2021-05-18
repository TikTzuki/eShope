using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Helpers
{
    public class Response<T>
    {
   public Response()
   {

   }
   public Response(T data)
   {
       Data=data;
   }
   public T Data{get;set;}
    }
}