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
 public class BrandsController : ControllerBase
 {
     private readonly EcommerContext _context;
     public BrandsController(EcommerContext context)
     {
         _context=context;
     }
     [HttpGet]
     public async Task<IEnumerable<brandDTO>> GetBrand()
     {
        return await _context.brands.Select(p=>p.toBrandDTO()).ToListAsync();
     }
     [HttpGet("{id}")]
     public async Task<ActionResult<brandDTO>> GetBrand(int id)
     {
      var b1=await _context.brands.FindAsync(id);
      if(b1==null)
      {
          return NotFound();
      }
      return b1.toBrandDTO();
     }
     [HttpPost]
     public async Task<ActionResult<brandDTO>> CreateProduct(brandDTO b2)
     {
         var b1=b2.toBrand();
         _context.brands.Add(b1);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(GetBrand),new {id=b1.id},b1);
     }
     [HttpPut("{id}")]
     public async Task<IActionResult> UpdateBrand(int id, brandDTO b2)
     {
         var b1=await _context.brands.FindAsync(b2.id);
         if(b1==null)
         {
             return NotFound();
         }
        b1.Mapto5(b2);
        _context.brands.Update(b1);
        try{
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException) when (!BrandExist(b1.id))
        {
            return NotFound();
        }
        return NoContent();
     }

     public bool BrandExist(int id)
     {
         return _context.brands.Any(p=>p.id==id);
     }
     [HttpDelete("{id}")]
     
     public async Task<IActionResult> DeleteBrand(int id)
     {
     var b1=await _context.brands.FindAsync(id);
     if(b1==null)
     {
         return NotFound();
     }
     _context.brands.Remove(b1);
     await _context.SaveChangesAsync();
     return NoContent();
     }
 }
}