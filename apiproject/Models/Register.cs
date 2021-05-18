using System;
using System.ComponentModel.DataAnnotations;
namespace Models
{
  public class Register
  {
    [Required(ErrorMessage = "Không được để trống tên")]
    public string name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "(Không được để trống email")]
    public string email { get; set; }
    public string phoneNumber { get; set; }
    [StringLength(20, MinimumLength = 5)]
    [Required(ErrorMessage = ("Không được để trống mật khẩu"))]
    public string password { get; set; }
    [Required(ErrorMessage = ("Khong được để trống xác nhận mật khẩu"))]
    [Compare("password", ErrorMessage = ("Mật khẩu không trùng khớp"))]
    public string confirmPassword { get; set; }
  }
}