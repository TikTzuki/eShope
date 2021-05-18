using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Models;

namespace Controllers
{
[Route("api/[controller]")]
    [ApiController]
 public class CustomersController : ControllerBase
 {
     private readonly EcommerContext _context;
     private readonly UserManager<Applicationuser> _userManager;
     public CustomersController(EcommerContext context, UserManager<Applicationuser> userManager)
     {
         _context=context;
         _userManager = userManager;
     }
     
     [HttpGet]
    public async Task<IEnumerable<CustomerDTO>> Getcustomer(string phoneNumber)
    {
      return await _context.customers
      .Select(p => p.tocustomerDTO()).ToListAsync();
    }
     
    [HttpGet("{id}")]
     public async Task<ActionResult<CustomerDTO>> Getcustomer(int id)
     {
      var cusDTO=_context.customers.Find(id).tocustomerDTO();
      if(cusDTO==null)
      {
          return NotFound();
      }
      cusDTO.address = await _context.address
      .Where(address => address.customerId == cusDTO.id)
      .Select(a => a.toAddressDTO()).ToListAsync();
      return cusDTO;
     }

    [Route("info")]
    [HttpGet]
    public async Task<ActionResult<CustomerDTO>> CustomerInfoFormToken()
    {
      var auth =  Request.Headers["Authorization"].ToString();
      var jwt = auth.Substring(auth.IndexOf(" ")+1);
      var handler = new JwtSecurityTokenHandler();
      var token = handler.ReadJwtToken(jwt);
      var subject = token.Subject;
      var cus = await _context.customers.Where(cus=>cus.phoneNumber==subject).FirstAsync();
      return cus!=null ? cus.tocustomerDTO() : NotFound();
    }

     [HttpPost]
     public async Task<ActionResult<CustomerDTO>> Createcustomer(CustomerDTO cus2)
     {
         var cus1=cus2.toCustomer();
         _context.customers.Add(cus1);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Getcustomer),new {id=cus1.id},cus1);
     }

     [HttpPut("{id}")]
     public async Task<IActionResult> Updatecustomer(int id, CustomerDTO customerDTO)
    {
      var customerEntity = await _context.customers.FindAsync(customerDTO.id);
      if (customerEntity == null)
      {
        return NotFound();
      }
      customerEntity.Mapto4(customerDTO);
      _context.customers.Update(customerEntity);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (_context.customers.Find(id) == null)
        {
          return NotFound();
        }
      }
      return NoContent();
    }

     [HttpDelete("{id}")]
     
     public async Task<IActionResult> DeleteCustomer(int id)
     {
     var cus1=await _context.customers.FindAsync(id);
     if(cus1==null)
     {
         return NotFound();
     }
     _context.customers.Remove(cus1);
     await _context.SaveChangesAsync();
     return NoContent();
     }
 }
}