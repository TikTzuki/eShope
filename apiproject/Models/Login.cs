using System;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class Login
    {
        [Required(ErrorMessage="Không được để trống password")]
        public String password{get;set;}
        [Required]
        public string phone{get;set;}
    }
}