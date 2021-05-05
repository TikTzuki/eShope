using System;
using System.ComponentModel.DataAnnotations;
namespace Models{
    public class Refreshtoken
    {
        public int id{get;set;}
        public String userName{get;set;}
        public String refreshToken{get;set;}
        public bool isrevoked{get;set;}
    }
}