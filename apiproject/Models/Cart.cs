using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Models{
    public class Cart{
        [Required]
        [Key]
        public int id{get;set;}
        [Required]
         public int customerId{get;set;}
          public decimal shippingFee{get;set;}
        public decimal totalPrice{get;set;} 
        public String items{get;set;}
        public virtual customer cus{get;set;}
    }
}