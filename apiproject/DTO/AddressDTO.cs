using System;
namespace DTO{
    public class AddressDTO{
         public int id{get;set;}
        public int customerId{get;set;}
        public String street{get;set;}
        public String address1{get;set;}
        public String address2{get;set;}
        public String address3{get;set;}
        public bool isDefault{get;set;}
    }
}