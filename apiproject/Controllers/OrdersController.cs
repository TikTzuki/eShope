using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Helpers;
using Filter;
using Baseurl;
using apiproject.Models.OrderAggregate;
using apiproject.DTO.InputModels;
using apiproject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly EcommerContext _context;
    private readonly IrulService _uriService;

    private readonly IOrderService _orderService;
    private readonly IConfiguration _config;
    public OrdersController(EcommerContext context, IrulService urlservice, IOrderService orderService, IConfiguration config)
    {
      _context = context;
      _uriService = urlservice;
      _orderService = orderService;
      _config = config;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] PagedFilter filter, [FromQuery] int customerId, [FromQuery] int id, [FromQuery] DateTime? createDate, [FromQuery] String paymentMethod, [FromQuery] decimal shippingFee, [FromQuery] String status)
    {
      var resfilter = new PagedFilter(filter.pageIndex, filter.pageSize);
      var orderDTOs = await _context.ordertable
      .Where(q =>
      (id != 0 ? q.id == id : true)
      && (createDate.HasValue ? DateTime.Compare(createDate.Value.Date, q.createDate.Date) == 0 : true)
      && (paymentMethod != null ? q.paymentMethod.Equals(paymentMethod) : true)
      && (shippingFee != 0 ? q.shippingFee == shippingFee : true)
      && (status != null ? q.status.Equals(status) : true)
      && (customerId != 0 ? q.customerId == customerId : true)
      )
      .Select(order => order.toorder_tblDTO())
      .ToListAsync();

      foreach (var orderDTO in orderDTOs)
      {
        var customerEntity = await _context.customers.FindAsync(orderDTO.customerId);
        orderDTO.customer = customerEntity.tocustomerDTO();
        var orderItemDTOs = await _context.orderitem.Where(item => item.orderId == orderDTO.id).Select(item => item.toorder_itemDTO()).ToListAsync();
        orderItemDTOs.ForEach(orderItemDTO =>
        {
          var skuDTO = _context.skus.Find(orderItemDTO.skuId).toskuDTO();
          skuDTO.images = _context.images.Where(image => image.skuId == skuDTO.id).Select(image => image.toimageDto()).ToList();
          orderItemDTO.sku = skuDTO;
        });
        orderDTO.orderItems = orderItemDTOs;
      }

      var totalcount = orderDTOs.Count();
      orderDTOs = orderDTOs.Skip(filter.pageIndex*filter.pageSize).Take(filter.pageSize).ToList();
      var route = Request.Path.Value;
      var pagedesponse = Pagedresponse.createpagedresponse<OrderTableDTO>(orderDTOs, filter, totalcount, _uriService, route);
      return Ok(pagedesponse);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderTableDTO>> GetOrder(int id)
    {
      var ord = await _context.ordertable.FindAsync(id);
      if (ord == null)
      {
        return NotFound();
      }
      var neword = new OrderTableDTO
      {
        id = ord.id,
        createDate = ord.createDate,
        updateDate = ord.updateDate,
        paymentMethod = ord.paymentMethod,
        shippingFee = ord.shippingFee,
        shippingAddress = ord.shippingAddress,
        totalPrice = ord.totalPrice,
        status = ord.status,
        customerId = ord.customerId,
        orderItems = _context.orderitem.Where(x => x.orderId == ord.id).Select(p => p.toorder_itemDTO())
      };
      return Ok(new Response<OrderTableDTO>(neword));
    }
    
    [HttpPost]
    public async Task<ActionResult<OrderTableDTO>> CreateOrder([FromBody] OrderTableDTO orderDTO)
    {
      var orderEntity = orderDTO.toOrder();
      foreach (var orderItem in orderDTO.orderItems)
      {
        var sku = _context.skus.Where(sku => sku.id == orderItem.skuId).First();
        sku.quantity -= orderItem.quantity;
        sku.available -= orderItem.quantity;
        if(sku.quantity < 0 || sku.available < 0){
          return BadRequest();
        }
        _context.skus.Update(sku);
      }
      _context.ordertable.Add(orderEntity);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetOrder), new { id = orderEntity.id }, orderEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderTableDTO tb2)
    {
      var table1 = await _context.ordertable.FindAsync(tb2.id);
      if (table1 == null)
      {
        return NotFound();
      }
      table1.Mapto2(tb2);
      _context.ordertable.Update(table1);
      await _context.SaveChangesAsync();
      return NoContent();
    }

  //TODO Abandoned
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
      var t1 = await _context.ordertable.FindAsync(id);
      if (t1 == null)
      {
        return NotFound();
      }
      _context.ordertable.Remove(t1);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    [HttpPost("payment")]
    public async Task<ActionResult<OrderPaymentIntent>> Payment(PaymentInput paymentInput){

      var order = await _context.ordertable.FindAsync(paymentInput.OrderId);
      if(order == null) return BadRequest();
      return await _orderService.CreatePaymentIntentAsync(order);
    }

    [HttpPost("webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> StripeWebhook()
    {
      var WhSecret = _config["StripeSettings:WebhookSecret"];
      var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
      var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

      Stripe.PaymentIntent intent;

      switch (stripeEvent.Type)
      {
        case "payment_intent.succeeded":
          intent = (Stripe.PaymentIntent)stripeEvent.Data.Object;
          Console.WriteLine(intent.ToString());
          Console.WriteLine(intent.Id);
          await _orderService.UpdateOrderPaymentSucceeded(intent.Id);
          break;
        case "payment_intent.payment_failed":
          intent = (Stripe.PaymentIntent)stripeEvent.Data.Object;
          _orderService.UpdateOrderPaymentFailed(intent.Id);
          break;
      }

      return new EmptyResult();
    }
  }
}