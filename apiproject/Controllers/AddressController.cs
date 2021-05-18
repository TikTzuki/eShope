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
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class AddressController : ControllerBase
  {
    private readonly EcommerContext _context;
    public AddressController(EcommerContext context)
    {
      _context = context;
    }
    [HttpGet]
    public async Task<IEnumerable<AddressDTO>> Getaddress([FromQuery] int customerId)
    {
      return await _context.address
      .Where(adress => customerId == 0 ? adress.customerId == customerId : true)
      .Select(p => p.toAddressDTO()).ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressDTO>> GetaddressUser(int id)
    {
      var b1 = await _context.address.FindAsync(id);
      if (b1 == null)
      {
        return NotFound();
      }
      return b1.toAddressDTO();
    }
    [HttpPost]
    public async Task<ActionResult<AddressDTO>> CreateAddress(AddressDTO addressDTO)
    {
      var addressEntity = addressDTO.toAddress();
      _context.address.Add(addressEntity);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(Getaddress), new { id = addressEntity.id }, addressEntity);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, AddressDTO b2)
    {
      var b1 = await _context.address.FindAsync(b2.id);
      if (b1 == null)
      {
        return NotFound();
      }
      b1.Mapto7(b2);
      _context.address.Update(b1);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!AddressExist(b1.id))
      {
        return NotFound();
      }
      return NoContent();
    }

    public bool AddressExist(int id)
    {
      return _context.address.Any(p => p.id == id);
    }
    [HttpDelete("{id}")]

    public async Task<IActionResult> Deleteaddress(int id)
    {
      var b1 = await _context.address.FindAsync(id);
      if (b1 == null)
      {
        return NotFound();
      }
      _context.address.Remove(b1);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}