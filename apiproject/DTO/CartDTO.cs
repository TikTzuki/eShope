using System;
namespace DTO{
    public class CartDTO{
        public int id{get;set;}
         public int customerId{get;set;}
          public decimal shippingFee{get;set;}
        public decimal totalPrice{get;set;} 
        public String items{get;set;}
    }
}