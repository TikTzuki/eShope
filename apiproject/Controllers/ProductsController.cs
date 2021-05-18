using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using DTO;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using DTO;
using Helpers;
using Filter;
using Baseurl;
using apiproject.Services;
using apiproject.Interfaces;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [CamelCaseControllerConfig]
  public class ProductsController : ControllerBase
  {
    private readonly EcommerContext _context;
    private readonly IrulService _uriservice;
    private readonly IProductService _service;
    public ProductsController(EcommerContext context, IrulService uriservice, IProductService service)
    {
      _context = context;
      this._uriservice = uriservice;
      _service = service;
    }

    [CamelCaseControllerConfig]
    [HttpGet]
    public async Task<IActionResult> GetProduct(
      [FromQuery] PagedFilter filter,
      [FromQuery] String productName,
      [FromQuery] int brandId,
      [FromQuery] int categoryId,
      [FromQuery] String status,
      [FromQuery] String minPrice,
      [FromQuery] String maxPrice)
    {
      var resfilter = new PagedFilter(filter.pageIndex, filter.pageSize);
      var productDTOs = await _service.GetAllAsync(productName, brandId, categoryId, status, minPrice, maxPrice);
      // var productDTOs = await _context.product
      // .Where(p =>
      //   (brandId != 0 ? p.brandId == brandId : true) &&
      //   (categoryId != 0 ? p.categoryId == categoryId : true)
      //   && (status != null ? p.status.Equals(status) : !p.status.Equals("deleted"))
      //   && (productName != null ? p.productName.ToLower().Contains(productName.ToLower()) : true)
      //   )
      // .Select(p => p.toProductDTO())
      // .ToListAsync();

      // //Add skus and image
      // productDTOs.ForEach(productDTO =>
      // {
      //   // Create skuDTOs
      //   var skuDTOs = _context.skus.Where(sku => sku.productId == productDTO.id).Select(sku => sku.toskuDTO()).ToList();
      //   skuDTOs.ForEach(skuDTO =>
      //   {
      //     // Create imageDTOs
      //     skuDTO.images = _context.images.Where(img => img.skuId == skuDTO.id).Select(img => img.toimageDto()).ToList();
      //   });
      //   productDTO.skus = skuDTOs;
      // });

      // // Filter price
      // productDTOs = productDTOs.Where(productDTO =>
      // {
      //   return productDTO.skus.Any(skuDTO =>
      //   (minPrice != null ? (skuDTO.price >= long.Parse(minPrice)) : true) &&
      //   (maxPrice != null ? (skuDTO.price <= long.Parse(maxPrice)) : true));
      // }).ToList();
     
      var totalcount = productDTOs.Count();
      productDTOs = productDTOs.Skip(filter.pageIndex * filter.pageSize).Take(filter.pageSize).ToList();
      var route = Request.Path.Value;
      var pagedesponse = Pagedresponse.createpagedresponse<ProductDTO>(productDTOs, filter, totalcount, _uriservice, route);
      return Ok(pagedesponse);
    }

    [CamelCaseControllerConfig]
    [HttpGet]
    [Route("catalog")]
    public async Task<IActionResult> GetCatalog(
      [FromQuery] PagedFilter filter,
      [FromQuery] String productName,
      [FromQuery] int brandId,
      [FromQuery] int categoryId,
      [FromQuery] String status,
      [FromQuery] String minPrice,
      [FromQuery] String maxPrice)
    {
      var resfilter = new PagedFilter(filter.pageIndex, filter.pageSize);
      var productDTOs = await _service.GetAllAsync(productName, brandId, categoryId, status, minPrice, maxPrice);
      // var productDTOs = await _context.product
      // .Where(p =>
      //   (brandId != 0 ? p.brandId == brandId : true) &&
      //   (categoryId != 0 ? p.categoryId == categoryId : true)
      //   && (status != null ? p.status.Equals(status) : !p.status.Equals("deleted"))
      //   && (productName != null ? p.productName.ToLower().Contains(productName.ToLower()) : true)
      //   )
      // .Select(p => p.toProductDTO())
      // .ToListAsync();

      // //Add skus and image
      // productDTOs.ForEach(productDTO =>
      // {
      //   // Create skuDTOs
      //   var skuDTOs = _context.skus.Where(sku => sku.productId == productDTO.id).Select(sku => sku.toskuDTO()).ToList();
      //   skuDTOs.ForEach(skuDTO =>
      //   {
      //     // Create imageDTOs
      //     skuDTO.images = _context.images.Where(img => img.skuId == skuDTO.id).Select(img => img.toimageDto()).ToList();
      //   });
      //   productDTO.skus = skuDTOs;
      // });
      // // Filter price
      // productDTOs = productDTOs.Where(productDTO =>
      // {
      //   return productDTO.skus.Any(skuDTO =>
      //   (minPrice != null ? (skuDTO.price >= long.Parse(minPrice)) : true) &&
      //   (maxPrice != null ? (skuDTO.price <= long.Parse(maxPrice)) : true));
      // }).ToList();
     
      var totalcount = productDTOs.Count();
      productDTOs = productDTOs.Skip(filter.pageIndex * filter.pageSize).Take(filter.pageSize).ToList();
      var route = Request.Path.Value;
      var pagedesponse = Pagedresponse.createpagedresponse<ProductDTO>(productDTOs, filter, totalcount, _uriservice, route);
      return Ok(pagedesponse);
    }

    [CamelCaseControllerConfig]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
      var productDTO = await _service.GetAsync(id);
      // var product = await _context.product.Where(p => 
      // p.id == id).FirstAsync();
      // var productDTO = product.toProductDTO();
      // var skuDTOs = await _context.skus.Where(sku => sku.productId == productDTO.id).Select(sku => sku.toskuDTO()).ToListAsync();
      // foreach(var skuDTO in skuDTOs){
      //   skuDTO.images = await _context.images.Where(img => img.skuId == skuDTO.id).Select(image=>image.toimageDto()).ToListAsync();
      // }
      // productDTO.skus = skuDTOs;
      // if (productDTO == null)
      // {
      //   return NotFound();
      // }
      return productDTO != null ? Ok(new Response<ProductDTO>(productDTO)) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO p2)
    {
      var p1 = await _service.CreateAsync(p2);
      return CreatedAtAction(nameof(GetProduct), new { id = p1.id }, p1);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Updateproduct(int id, [FromBody] ProductDTO productForm)
    {

      // var productEntity = await _context.product.FindAsync(productForm.id);
      // if (productEntity == null)
      // {
      //   return NotFound();
      // }
      // productEntity.Mapto6(productForm);
      // _context.product.Update(productEntity);
      // try
      // {
      //   await _context.SaveChangesAsync();
      // }
      // catch (DbUpdateConcurrencyException) when (!ProductExist(productEntity.id))
      // {
      //   return NotFound();
      // }
      // return Ok();
      return await _service.Update(id, productForm) ? Ok() : BadRequest();
    }

    public bool ProductExist(int id)
    {
      return _context.product.Any(p => p.id == id);
    }

  //TODO abandoned
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
      var p1 = await _context.product.FindAsync(id);
      if (p1 == null)
      {
        return NotFound();
      }
      _context.product.Remove(p1);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}