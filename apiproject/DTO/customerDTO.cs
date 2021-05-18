using System;
using System.Collections.Generic;
namespace DTO{
    public class CustomerDTO
  {
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string password { get; set; }
    public string accessToken { get; set; }
    public DateTime accesExpire { get; set; }
    public string refreshToken { get; set; }

    public string gender { get; set; }
    public IEnumerable<AddressDTO> address { get; set; }
  }
}