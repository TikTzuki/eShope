using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Models
{
  public class Seller
  {
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public String phoneNumber { get; set; }
    [Required]
    public String name { get; set; }
    [Required]
    [EmailAddress]
    public String email { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 8)]
    [Compare("Password")]
    [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự và thỏa 3 điều kiện sau:1.Phải có ký tự viết hoa,2.Phải chứa chữ số,3.Phải chưa ký tự đặc biệt.")]
    public String password { get; set; }
    public String secretKey { get; set; }
    public String accessToken { get; set; }
    public DateTime accessExpire { get; set; }
    public String refreshToken { get; set; }
  }
}