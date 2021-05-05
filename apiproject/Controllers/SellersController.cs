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
using System.IdentityModel.Tokens.Jwt;
using Helpers;

namespace Controllers
{
[Route("api/[controller]")]
    [ApiController]
 public class SellersController : ControllerBase
 {
     private readonly EcommerContext _context;
     public SellersController(EcommerContext context)
     {
         _context=context;
     }

     [HttpGet]
     public async Task<IEnumerable<SellerDTO>> GetSeller()
     {
         
        var sellerDTOs = _context.seller.Select(p=>p.toSellerDTO()).ToList();
        sellerDTOs.ForEach(sellerDTO=>{
            sellerDTO.address = _context.selleraddresses.Where(Address=>Address.sellerId==sellerDTO.id).Select(a=>a.toSellerAddressDTO()).ToList();
        });
        return sellerDTOs;
     }

     [HttpGet("{id}")]
     public async Task<ActionResult<SellerDTO>> GetSeller(int id)
     {
      var sellerDTO= _context.seller.Find(id).toSellerDTO();
      if(sellerDTO==null)
      {
          return NotFound();
      }
      sellerDTO.address = _context.selleraddresses
      .Where(address=>address.sellerId==sellerDTO.id)
      .Select(a=>a.toSellerAddressDTO()).ToList();
      return sellerDTO;
     }

    [Route("info")]
    [HttpGet]
    public async Task<ActionResult<SellerDTO>> SellerInfoFormToken()
    {
      var subject = TokenUtil.getSubject(Request);
      var seller = await _context.seller.Where(seller => seller.phoneNumber.Equals(subject)).FirstAsync();
      return Ok(seller);
    }

     [HttpPost]
     public async Task<ActionResult<SellerDTO>> CreateSeller(SellerDTO s)
     {
         var s1=s.toSeller();
         _context.seller.Add(s1);
         await _context.SaveChangesAsync();
         return CreatedAtAction(nameof(GetSeller),new {id=s1.id},s1);
     }

     [HttpPut("{id}")]
     public async Task<IActionResult> Updateseller(SellerDTO s)
     {
         var s1=await _context.seller.FindAsync(s.id);
         if(s1==null)
         {
             return NotFound();
         }
        s1.Mapto9(s);
        _context.seller.Update(s1);
        try{
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException) when (!SelExist(s1.id))
        {
            return NotFound();
        }
        return NoContent();
     }

     public bool SelExist(int id)
     {
         return _context.seller.Any(p=>p.id==id);
     }
     [HttpDelete("{id}")]
     
     public async Task<IActionResult> DeleteSeller(int id)
     {
     var s1=await _context.seller.FindAsync(id);
     if(s1==null)
     {
         return NotFound();
     }
     _context.seller.Remove(s1);
     await _context.SaveChangesAsync();
     return NoContent();
     }
 }
}