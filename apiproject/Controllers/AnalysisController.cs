using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Models;
using apiproject.Models.OrderAggregate;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class AnalysisController : ControllerBase
  {
    private readonly EcommerContext _context;
    public AnalysisController(EcommerContext context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("daily-task")]
    public async Task<ActionResult> getDailyTaskList(int shortStockValue = 5)
    {
      var dailyTask = new DailyTask(
       await _context.ordertable.Where(o => o.status.Equals(EOrderStatus.pending)).CountAsync(),
       await _context.ordertable.Where(o => o.status.Equals(EOrderStatus.unpaid)).CountAsync(),
       await _context.skus.Where(sku => sku.quantity <= shortStockValue || sku.available <= shortStockValue).CountAsync()
      );
      return Ok(dailyTask);
    }
  }
  class DailyTask
  {
    public DailyTask(int pendingOrder, int unpaidOrder, int shortOfStockProduct)
    {
      this.pendingOrders = pendingOrder;
      this.unpaidOrders = unpaidOrder;
      this.shortOfStockProducts = shortOfStockProduct;

    }
    public int pendingOrders { get; set; }
    public int unpaidOrders { get; set; }
    public int shortOfStockProducts { get; set; }
  }
}