using System;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class order_item
    {[Required]
    [Key]
           public int id{get;set;}
       [Required]
        public int orderId{get;set;}
        public int skuId{get;set;}
        [StringLength(60,MinimumLength=10)]
        public string name{get;set;}
        public string variation{get;set;}
        public decimal price{get;set;}
        public int quantity {get;set;}
        public string image{get;set;}
        public virtual Order order{get;set;}
        public virtual Sku _sku{get;set;}
    }
}