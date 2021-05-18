using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Models{
    public class customer
    {    [Required]
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id{get;set;}
        public string name{get;set;}
        [EmailAddress]
        public string email{get;set;}

        public string phoneNumber{get;set;}
        [Compare("Password")]
         [StringLength(20, MinimumLength = 8)]
           [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự và thỏa 3 điều kiện sau:1.Phải có ký tự viết hoa,2.Phải chứa chữ số,3.Phải chưa ký tự đặc biệt.")] 
        public string password{get;set;}
        public string accessToken{get;set;}
        public DateTime accesExpire {get;set;}
        public string refreshToken{get;set;}
        public string gender{get;set;}
    }
}
