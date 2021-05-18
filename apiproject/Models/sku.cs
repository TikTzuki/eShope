using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Models{
    public class Sku{
   [Required]
   [Key]
    public int id {get;set;}
       [StringLength(60, MinimumLength = 3)]
        [Display(Name="Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string sellerSku {get;set;}
        [Required]
        public int productId{get;set;}
        public int available {get;set;}
        public int quantity {get;set;} 
        public string color{get;set;}
        public int size{get;set;}
        public int height{get;set;}
        public int width{get;set;}
        public int length{get;set;}
        public int weight{get;set;}
        [RegularExpression(@"\D+")]
        public decimal price{get;set;}
        public IEnumerable<images> images{get;set;}
        public virtual Products product{get;set;}
    }
}