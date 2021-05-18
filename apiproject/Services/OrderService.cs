using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiproject.Interfaces;
using apiproject.Models.OrderAggregate;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Data;
using Order = Models.Order;
using Stripe;

namespace apiproject.Services
{
  public class OrderService : IOrderService
  {
      private readonly IConfiguration _config;
      private readonly EcommerContext _context;
      public OrderService(EcommerContext context ,IConfiguration config){
          _context = context;
          _config = config;
      }
    public Task<Order> CreateOrderAsync(Order orderInput)
    {
      throw new System.NotImplementedException();
    }

    public async Task<OrderPaymentIntent> CreatePaymentIntentAsync(Order order)
    {
      StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
      var service = new PaymentIntentService();
      var options = new PaymentIntentCreateOptions
      {
        Amount = Convert.ToInt64(order.totalPrice),
        Currency = "usd",
        PaymentMethodTypes = new List<string> { "card" }
      };

      var intent = await service.CreateAsync(options);
      order.PaymentIntent = new OrderPaymentIntent
      {
        PaymentIntentId = intent.Id,
        ClientSecret = intent.ClientSecret
      };

      _context.ordertable.Update(order);
      await _context.SaveChangesAsync();

      return order.PaymentIntent;
    }

    public Task<Order> GetOrderByIdAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateOrderPaymentFailed(string paymentIntentId)
    {
       Console.WriteLine("Failed, PaymentIntentId: " + paymentIntentId);
    }

    public async Task<bool> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
      Console.WriteLine("Succeeded, PaymentIntentId: " + paymentIntentId);
      var order = _context.ordertable.Where(o => o.status == EOrderStatus.unpaid && o.PaymentIntent.PaymentIntentId.Equals(paymentIntentId)).First();
      // foreach (var orderEntity in orders)
      // {
      //   var PaymentIntent = _context.paymentintent.Where(p => p.PaymentIntentId.Equals(orderEntity.PaymentIntent.PaymentIntentId)).First();
      //   if (PaymentIntent != null)
      //     orderEntity.PaymentIntent = PaymentIntent;
      // }
      // var order = orders.Where(o => o.PaymentIntent.PaymentIntentId.Equals(paymentIntentId)).First(); 
      if (order == null) return false;
      //Change status
      order.status = EOrderStatus.pending;
      _context.ordertable.Update(order);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}