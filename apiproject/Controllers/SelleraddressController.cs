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
namespace Controllers
{
[Route("api/[controller]")]
    [ApiController]
 public class SellerAddressController : ControllerBase
 {
     private readonly EcommerContext _context;
     public SellerAddressController(EcommerContext context)
     {
         _context=context;
     }

     [HttpGet]
     public async Task<IEnumerable<SelleraddressDTO>> Getaddress()
     {
        return await _context.selleraddresses.Select(p=>p.toSellerAddressDTO()).ToListAsync();
     }

     [HttpGet("{id}")]
     public async Task<ActionResult<SelleraddressDTO>> Getaddress(int id)
     {
      var sel1=await _context.selleraddresses.FindAsync(id);
      if(sel1==null)
      {
          return NotFound();
      }
      return sel1.toSellerAddressDTO();
     }

     [HttpPost]
     public async Task<ActionResult<SelleraddressDTO>> Createaddress(SelleraddressDTO addressDTO)
     {
         var addressEntity=addressDTO.toselleraddress();
         _context.selleraddresses.Add(addressEntity);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Getaddress),new {id=addressEntity.id},addressEntity);
     }

     [HttpPut("{id}")]
     public async Task<IActionResult> Updateaddress(int id, SelleraddressDTO addressDTO)
     {
         var addressEntity=await _context.selleraddresses.FindAsync(addressDTO.id);
         if(addressEntity==null)
         {
             return NotFound();
         }
        addressEntity.Mapto10(addressDTO);
        var tracking = _context.selleraddresses.Update(addressEntity);
            await _context.SaveChangesAsync();
        return Ok(addressDTO);
     }

     [HttpDelete("{id}")]
     
     public async Task<IActionResult> Deleteaddress(int id)
     {
     var s1=await _context.selleraddresses.FindAsync(id);
     if(s1==null)
     {
         return NotFound();
     }
     _context.selleraddresses.Remove(s1);
     await _context.SaveChangesAsync();
     return NoContent();
     }
 }
}