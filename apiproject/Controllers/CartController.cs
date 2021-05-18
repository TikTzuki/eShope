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
using Helpers;
using Models;

namespace Controllers
{
[Route("api/customer/[controller]")]
    [ApiController]
 public class CartController : ControllerBase
 {
     private readonly EcommerContext _context;
     public CartController(EcommerContext context)
     {
         _context=context;
     }

    //  [HttpGet]
    //  public async Task<IEnumerable<CartDTO>> GetCart()
    //  {
    //     return await _context.cart.Select(p=>p.toCartDto()).ToListAsync();
    //  }

     [HttpGet]
     public async Task<ActionResult<CartDTO>> GetCart()
    {
      var subject = TokenUtil.getSubject(Request);
      var customer = _context.customers.Where(c => c.phoneNumber == subject).First();
      var cartOptional = await _context.cart.Where(cart => cart.customerId == customer.id).ToListAsync();
      if (cartOptional.Count() == 0)
      {
        var newCart = new Cart();
        newCart.customerId = customer.id;
        newCart.items = "[]";
        _context.cart.Add(newCart);
        _context.SaveChanges();
        return newCart.toCartDto();
      }
      return cartOptional.First().toCartDto();
    }

    //  [HttpPost]
    //  public async Task<ActionResult<CartDTO>> Createcart(CartDTO c)
    //  {
    //      var c1=c.tocart();
    //      _context.cart.Add(c1);
    //      await _context.SaveChangesAsync();
    //      return CreatedAtAction(nameof(GetCart),new {id=c1.id},c1);
    //  }

    [HttpPut("{id}")]
    public async Task<IActionResult> Updatecart(int id, CartDTO cart)
    {
      var cartEntity = await _context.cart.FindAsync(cart.id);
      if (cartEntity == null)
      {
        return NotFound();
      }
      cartEntity.Mapto11(cart);
      _context.cart.Update(cartEntity);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!cartExist(cartEntity.id))
      {
        return NotFound();
      }
      return NoContent();
    }

     public bool cartExist(int id)
     {
         return _context.cart.Any(p=>p.id==id);
     }
    //  [HttpDelete("{id}")]
     
    //  public async Task<IActionResult> Deletecart(int id)
    //  {
    //  var s1=await _context.cart.FindAsync(id);
    //  if(s1==null)
    //  {
    //      return NotFound();
    //  }
    //  _context.cart.Remove(s1);
    //  await _context.SaveChangesAsync();
    //  return NoContent();
    //  }
 }
}