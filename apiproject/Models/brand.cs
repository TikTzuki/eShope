using System;
using System.ComponentModel.DataAnnotations;
namespace Models{
    public class brand
    {    [Key]
        public int id{get;set;}
        public string name{get;set;}
    }
}