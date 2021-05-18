using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DTO;
using Filter;
using Baseurl;
using Helpers;
namespace Controllers
{
[Route("api/[controller]")]
  [ApiController]
 public class OrderitemsController : ControllerBase
 {
     private readonly EcommerContext _context;
     private readonly IrulService _urlservice;
     public OrderitemsController(EcommerContext context,IrulService urlservice)
     {
         _context=context;
         _urlservice=urlservice;
     }

     [HttpGet]
     public async Task<IActionResult> GetOrderItems([FromQuery]PagedFilter filter1)
     {  
        var filter=new PagedFilter(filter1.pageIndex,filter1.pageSize);
        var data=await _context.orderitem.Select(p=>p.toorder_itemDTO()).ToListAsync();
        return Ok(data);
     }

     [HttpGet("{id}")]
     public async Task<ActionResult<order_itemDTO>> GetOrderItem(int id)
     {
      var or=await _context.orderitem.FindAsync(id);
      if(or==null)
      {
          return NotFound();
      }
      return Ok(new Response<order_itemDTO>(or.toorder_itemDTO()));
     }

     [HttpPost]
     public async Task<ActionResult<order_itemDTO>> CreateItem(order_itemDTO or2)
     {
         var or1=or2.toorder_item();
         _context.orderitem.Add(or1);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(GetOrderItem),new {id=or1.id},or1);
     }

     [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, order_itemDTO or2)
    {
      var or1 = await _context.orderitem.FindAsync(or2.id);
      if (or1 == null)
      {
        return NotFound();
      }
      or1.Mapto1(or2);
      _context.orderitem.Update(or1);
      await _context.SaveChangesAsync();
      return NoContent();
    }

  //TODO Anbandon
     [HttpDelete("{id}")]
     
     public async Task<IActionResult> Deleteprderitem(int id)
     {
     var or1=await _context.orderitem.FindAsync(id);
     if(or1==null)
     {
         return NotFound();
     }
     _context.orderitem.Remove(or1);
     await _context.SaveChangesAsync();
     return NoContent();
     }
 }
}