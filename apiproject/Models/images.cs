using System;
using System.ComponentModel.DataAnnotations;
namespace Models{
    public class images
    {
        public string url{get;set;}
        public int skuId{get;set;}
        [Key]
        [Required]
        public int id{get;set;}
       public virtual Sku _sku{get;set;}
       
    }
}