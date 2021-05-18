using System.Collections.Generic;
using System.Threading.Tasks;
using apiproject.Models.OrderAggregate;
using Models;

namespace apiproject.Interfaces
{
  public interface IOrderService
  {
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username);
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order orderInput);
    Task<OrderPaymentIntent> CreatePaymentIntentAsync(Order order);
    Task<bool> UpdateOrderPaymentSucceeded(string paymentIntentId);
    void UpdateOrderPaymentFailed(string paymentIntentId);
  }
}