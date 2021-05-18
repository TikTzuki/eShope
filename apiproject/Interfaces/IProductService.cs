using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiproject.Models.OrderAggregate;
using DTO;
using Models;

namespace apiproject.Interfaces
{
  public interface IProductService
  {
    Task<Products> CreateAsync(ProductDTO productFrom);

    Task<ProductDTO> GetAsync(int id);

    Task<IEnumerable<ProductDTO>> GetAllAsync(
      String productName,
      int brandId,
      int categoryId,
      String status,
      String minPrice,
      String maxPrice
    );

    Task<bool> Update(int id, ProductDTO productForm);

  }
}