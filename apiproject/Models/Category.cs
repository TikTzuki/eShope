using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Models{
    public class Category{
         [Key]
        public int id{get;set;}
        public String name{get;set;}
    }
}