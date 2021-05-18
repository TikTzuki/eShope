using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiproject.Interfaces;
using apiproject.Models.OrderAggregate;
using Data;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace apiproject.Services
{
  public class ProductService : IProductService
  {
    private readonly IConfiguration _config;
    private readonly EcommerContext _context;

    public ProductService(EcommerContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    public async Task<Products> CreateAsync(ProductDTO productFrom)
    {
      var p1 = productFrom.toProduct();
      _context.product.Add(p1);
      await _context.SaveChangesAsync();
      return p1;
    }

    public async Task<ProductDTO> GetAsync(int id)
    {
      var product = await _context.product.Where(p => p.id == id).FirstAsync();
      var productDTO = product.toProductDTO();
      // Add skus
      var skuDTOs = await _context.skus.Where(sku => sku.productId == productDTO.id).Select(sku => sku.toskuDTO()).ToListAsync();
      foreach (var skuDTO in skuDTOs)
      {
        skuDTO.images = await _context.images.Where(img => img.skuId == skuDTO.id).Select(image => image.toimageDto()).ToListAsync();
      }
      productDTO.skus = skuDTOs;
      // Add Brand
      productDTO.brand = await _context.brands.Where(b => b.id == productDTO.brandId).Select(b => b.toBrandDTO()).FirstAsync();
      return productDTO;
    }
    public async Task<IEnumerable<ProductDTO>> GetAllAsync(
      String productName,
      int brandId,
      int categoryId,
      String status,
      String minPrice,
      String maxPrice
      )
    {
      var productDTOs = await _context.product
      .Where(p =>
        (brandId != 0 ? p.brandId == brandId : true) &&
        (categoryId != 0 ? p.categoryId == categoryId : true)
        && (status != null ? p.status.Equals(status) : !p.status.Equals("deleted"))
        && (productName != null ? p.productName.ToLower().Contains(productName.ToLower()) : true)
        )
      .Select(p => p.toProductDTO())
      .ToListAsync();
      //Add skus and image
      productDTOs.ForEach(productDTO =>
      {
        // Create skuDTOs
        var skuDTOs = _context.skus.Where(sku => sku.productId == productDTO.id).Select(sku => sku.toskuDTO()).ToList();
        skuDTOs.ForEach(skuDTO =>
        {
          // Create imageDTOs
          skuDTO.images = _context.images.Where(img => img.skuId == skuDTO.id).Select(img => img.toimageDto()).ToList();
        });
        productDTO.skus = skuDTOs;
      });
      // Filter price
      productDTOs = productDTOs.Where(productDTO =>
      {
        return productDTO.skus.Any(skuDTO =>
        (minPrice != null ? (skuDTO.price >= long.Parse(minPrice)) : true) &&
        (maxPrice != null ? (skuDTO.price <= long.Parse(maxPrice)) : true));
      }).ToList();

      return productDTOs;
    }

    public async Task<bool> Update(int id, ProductDTO productForm)
    {
      var productEntity = await _context.product.FindAsync(productForm.id);
      if (productEntity == null)
      {
        return false;
      }
      productEntity.Mapto6(productForm);
      _context.product.Update(productEntity);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!ProductExist(productEntity.id))
      {
        return false;
      }
      return true;
    }

    public bool ProductExist(int id)
    {
      return _context.product.Any(p => p.id == id);
    }

  }

}