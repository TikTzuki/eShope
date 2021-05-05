using System;
namespace DTO{
    public class order_itemDTO
    {
          public int id{get;set;}
        public int orderId{get;set;}
        public int skuId{get;set;}
        public string name{get;set;}
        public string variation{get;set;}
        public decimal price{get;set;}
        public int quantity {get;set;}
        public string image{get;set;}
        public SkuDTO sku {get;set;}
    }
}