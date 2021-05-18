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
 public class CategoryController : ControllerBase
 {
     private readonly EcommerContext _context;
     public CategoryController(EcommerContext context)
     {
         _context=context;
     }
     [HttpGet]
     public async Task<IEnumerable<CategoryDTO>> Getcategory()
     {
        return await _context.category.Select(p=>p.toCategoryDTO()).ToListAsync();
     }
     [HttpGet("{id}")]
     public async Task<ActionResult<CategoryDTO>> Getcategory(int id)
     {
      var b1=await _context.category.FindAsync(id);
      if(b1==null)
      {
          return NotFound();
      }
      return b1.toCategoryDTO();
     }
     [HttpPost]
     public async Task<ActionResult<CategoryDTO>> Createcategory(CategoryDTO c2)
     {
         var c1=c2.toCategory();
         _context.category.Add(c1);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(Getcategory),new {id=c1.id},c1);
     }
     [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, CategoryDTO b2)
     {
         var b1=await _context.category.FindAsync(b2.id);
         if(b1==null)
         {
             return NotFound();
         }
        b1.Mapto8(b2);
        _context.category.Update(b1);
        try{
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException) when (!categoryExist(b1.id))
        {
            return NotFound();
        }
        return NoContent();
     }

     public bool categoryExist(int id)
     {
         return _context.category.Any(p=>p.id==id);
     }
     [HttpDelete("{id}")]
     
     public async Task<IActionResult> Deletecategory(int id)
     {
     var b1=await _context.category.FindAsync(id);
     if(b1==null)
     {
         return NotFound();
     }
     _context.category.Remove(b1);
     await _context.SaveChangesAsync();
     return NoContent();
     }
 }
}