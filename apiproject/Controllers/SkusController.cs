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
using Filter;
using Baseurl;
namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [CamelCaseControllerConfig]
  public class SkusController : ControllerBase
  {
    private readonly EcommerContext _context;
    private readonly IrulService _uriservice;
    public SkusController(EcommerContext context, IrulService uriservice)
    {
      _context = context;
      this._uriservice = uriservice;
    }
    [HttpGet]
    [CamelCaseControllerConfig]
    public async Task<IActionResult> GetSkus([FromQuery] PagedFilter filter)
    {
      var resfilter = new PagedFilter(filter.pageIndex, filter.pageSize);
      List<SkuDTO> d = new List<SkuDTO>();
      var data2 = await _context.skus.ToListAsync();
      foreach (var sku1 in data2)
      {
        var newsku = new SkuDTO
        {
          id = sku1.id,
          sellerSku = sku1.sellerSku,
          available = sku1.available,
          quantity = sku1.quantity,
          color = sku1.color,
          size = sku1.size,
          height = sku1.height,
          width = sku1.width,
          length = sku1.length,
          weight = sku1.weight,
          price = sku1.price,
          productId = sku1.productId,
          images = _context.images.Where(x => x.skuId == sku1.id).Select(p => p.toimageDto())
        };
        d.Add(newsku);
      }
      var data = await _context.skus.Select(p => p.toskuDTO()).ToListAsync();
      return Ok(data);
    }

    [HttpGet("{id}")]
    [CamelCaseControllerConfig]
    public async Task<ActionResult<SkuDTO>> Getsku(int id)
    {
      var sku1 = await _context.skus.FindAsync(id);
      if (sku1 == null)

      {
        return NotFound();
      }
      var newsku = new SkuDTO
      {
        id = sku1.id,
        sellerSku = sku1.sellerSku,
        available = sku1.available,
        quantity = sku1.quantity,
        color = sku1.color,
        size = sku1.size,
        height = sku1.height,
        width = sku1.width,
        length = sku1.length,
        weight = sku1.weight,
        price = sku1.price,
        productId = sku1.productId,
        images = _context.images.Where(x => x.skuId == sku1.id).Select(p => p.toimageDto())
      };
      return Ok(new Response<SkuDTO>(newsku));
    }

    [HttpPost]
    public async Task<ActionResult<SkuDTO>> CreateProduct(SkuDTO sku2)
    {
      var sku1 = sku2.tosku();
      _context.skus.Add(sku1);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(Getsku), new { id = sku1.id }, sku1);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Updateproduct(int id, SkuDTO sku2)
    {
      var sku1 = await _context.skus.FindAsync(sku2.id);
      if (sku1 == null)
      {
        return NotFound();
      }
      sku1.Mapto(sku2);
      _context.skus.Update(sku1);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!SkuExist(sku1.id))
      {
        return NotFound();
      }
      return NoContent();
    }

    public bool SkuExist(int id)
    {
      return _context.skus.Any(p => p.id == id);
    }
    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteProduct(int id)
    {
      var sku1 = await _context.skus.FindAsync(id);
      if (sku1 == null)
      {
        return NotFound();
      }
      _context.skus.Remove(sku1);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}