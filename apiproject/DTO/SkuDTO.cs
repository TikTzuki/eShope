using System;
using System.Collections.Generic;
namespace DTO
{
    
public class SkuDTO
{      public int id {get;set;}
        public string sellerSku {get;set;}
    
        public int available {get;set;}
        public int quantity {get;set;}
        public string color{get;set;}
        public int size{get;set;}
        public int height{get;set;}
        public int width{get;set;}
        public int length{get;set;}
        public int weight{get;set;}
        public decimal price{get;set;}
        public IEnumerable<imageDTO> images{get;set;}
        public int productId{get;set;}
}
}