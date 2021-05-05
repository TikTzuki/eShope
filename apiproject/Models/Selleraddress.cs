using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Models
{
  public class Selleraddress
  {
    [Key]
    [Required]
    public int id { get; set; }
    [Required]
    public int sellerId { get; set; }
    public String street { get; set; }
    [EmailAddress]
    public String address1 { get; set; }
    [EmailAddress]
    public String address2 { get; set; }
    [EmailAddress]
    public String address3 { get; set; }
    public bool isDefault {get; set;}
    public virtual Seller seller { get; set; }
  }
}