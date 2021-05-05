using System.ComponentModel.DataAnnotations;

namespace apiproject.Models.OrderAggregate
{
  public class OrderPaymentIntent
  {
    [Key]
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
  }
}