
namespace apiproject.Models.OrderAggregate
{
  public class EOrderStatus{
    public const string unpaid = "unpaid";
    public const string pending = "pending";
    public const string rts = "rts";
    public const string canceled = "canceled";
    public const string shipped = "shipped";
    public const string delivered = "delivered";
  }
}