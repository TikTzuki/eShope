using System.Security.Cryptography;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Models;

namespace DTO
{
  public static class MappingProfile
  {
    public static EcommerContext _context;
    public static SkuDTO toskuDTO(this Sku sku1)
    {
      return new SkuDTO()
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
        productId = sku1.productId
      };

    }
    public static Sku tosku(this SkuDTO sku2)
    {
      return new Sku()
      {
        id = sku2.id,
        sellerSku = sku2.sellerSku,
        available = sku2.available,
        quantity = sku2.quantity,
        color = sku2.color,
        size = sku2.size,
        height = sku2.height,
        width = sku2.width,
        length = sku2.length,
        weight = sku2.weight,
        price = sku2.price,
        productId = sku2.productId
      };
    }
    public static void Mapto(this Sku sku1, SkuDTO sku2)
    {
      sku1.id = sku2.id;
      sku1.sellerSku = sku2.sellerSku;
      sku1.available = sku2.available;
      sku1.quantity = sku2.quantity;
      sku1.color = sku2.color;
      sku1.size = sku2.size;
      sku1.height = sku2.height;
      sku1.width = sku2.width;
      sku1.length = sku2.length;
      sku1.weight = sku2.weight;
      sku1.price = sku2.price;
      sku1.productId = sku2.productId;

    }
    public static order_itemDTO toorder_itemDTO(this order_item ori1)
    {
      return new order_itemDTO()
      {
        id = ori1.id,
        orderId = ori1.orderId,
        skuId = ori1.skuId,
        name = ori1.name,
        variation = ori1.variation,
        price = ori1.price,
        quantity = ori1.quantity,
        image = ori1.image
      };
    }
    public static order_item toorder_item(this order_itemDTO ori1)
    {
      return new order_item()
      {
        id = ori1.id,
        orderId = ori1.orderId,
        skuId = ori1.skuId,
        name = ori1.name,
        variation = ori1.variation,
        price = ori1.price,
        quantity = ori1.quantity,
        image = ori1.image
      };
    }
    public static void Mapto1(this order_item ori, order_itemDTO ori1)
    {

      ori.id = ori1.id;
      ori.orderId = ori1.orderId;
      ori.skuId = ori1.skuId;
      ori.name = ori1.name;
      ori.variation = ori1.variation;
      ori.price = ori1.price;
      ori.quantity = ori1.quantity;
    }
    public static OrderTableDTO toorder_tblDTO(this Order ord)
    {
      return new OrderTableDTO()
      {
         id=ord.id,
        createDate = ord.createDate,
        updateDate = ord.updateDate,
        paymentMethod = ord.paymentMethod,
        shippingFee = ord.shippingFee,
        shippingAddress = ord.shippingAddress,
        totalPrice = ord.totalPrice,
        status = ord.status,
        customerId=ord.customerId
      };
    }
    public static Order toOrder(this OrderTableDTO ord1)
    {
      return new Order()
      {
          id=ord1.id,
        createDate = ord1.createDate,
        updateDate = ord1.updateDate,
        paymentMethod = ord1.paymentMethod,
        shippingFee = ord1.shippingFee,
        shippingAddress = ord1.shippingAddress,
        totalPrice = ord1.totalPrice,
        status = ord1.status,
        customerId=ord1.customerId
      };
    }
    public static void Mapto2(this Order ord, OrderTableDTO ord1)
    {
      ord.id = ord1.id;
      ord.createDate = ord1.createDate;
      ord.updateDate = ord1.updateDate;
      ord.paymentMethod = ord1.paymentMethod;
      ord.shippingFee = ord1.shippingFee;
      ord.shippingAddress = ord1.shippingAddress;
      ord.totalPrice = ord1.totalPrice;
      ord.status = ord1.status;
      ord.customerId=ord1.customerId;
    }
    public static imageDTO toimageDto(this images img)
    {
      return new imageDTO()
      { id=img.id,
        url = img.url,
        skuId = img.skuId

      };
    }
    public static images toimage(this imageDTO img1)
    {
      return new images()
      { id=img1.id,
        url = img1.url,
        skuId = img1.skuId

      };
    }
    public static void Mapto3(this images img, imageDTO img1)
    {
      img.url = img1.url;
      img.skuId = img1.skuId;
    }
    public static CustomerDTO tocustomerDTO(this customer c)
    {
      return new CustomerDTO()
      {
        id = c.id,
        name = c.name,
        email = c.email,
        phoneNumber = c.phoneNumber,
        password = c.password,
        accessToken = c.accessToken,
        accesExpire = c.accesExpire,
        refreshToken = c.refreshToken,
        gender = c.gender
      };
    }
    public static customer toCustomer(this CustomerDTO c1)
    {
      return new customer()
      {
        id = c1.id,
        name = c1.name,
        email = c1.email,
        phoneNumber = c1.phoneNumber,

        password = c1.password,
        accessToken = c1.accessToken,
        accesExpire = c1.accesExpire,
        refreshToken = c1.refreshToken
      };
    }
    public static void Mapto4(this customer c, CustomerDTO c1)
    {
      c.id = c1.id;
      c.name = c1.name;
      c.email = c1.email;
      c.phoneNumber = c1.phoneNumber;
      c.gender = c1.gender;
      c.password = c1.password;
      c.accessToken = c1.accessToken;
      c.accesExpire = c1.accesExpire;
      c.refreshToken = c1.refreshToken;

    }
    public static brand toBrand(this brandDTO b)
    {
      return new brand()
      {
        id = b.id,
        name = b.name
      };

    }
    public static brandDTO toBrandDTO(this brand b1)
    {
      return new brandDTO()
      {
        id = b1.id,
        name = b1.name
      };
    }
    public static void Mapto5(this brand b, brandDTO b1)
    {
      b.id = b1.id;
      b.name = b1.name;
    }
    public static ProductDTO toProductDTO(this Products pr1)
    {
      return new ProductDTO()
      {
        id = pr1.id,
        categoryId = pr1.categoryId,
        brandId = pr1.brandId,
        productName = pr1.productName,
        description = pr1.description,
        status = pr1.status
      };
    }
    public static Products toProduct(this ProductDTO pr2)
    {
      return new Products()
      {
        id = pr2.id,
        categoryId = pr2.categoryId,
        brandId = pr2.brandId,
        productName = pr2.productName,
        description = pr2.description,
        status = pr2.status
      };
    }
    public static void Mapto6(this Products pr1, ProductDTO pr2)
    {
      pr1.id = pr2.id;
      pr1.categoryId = pr2.categoryId;
      pr1.brandId = pr2.brandId;
      pr1.productName = pr2.productName;
      pr1.description = pr2.description;
      pr1.status = pr2.status;
    }
    public static AddressDTO toAddressDTO(this Address ad)
    {
      return new AddressDTO()
      {
        id = ad.id,
        customerId = ad.customerId,
        street = ad.street,
        address1 = ad.address1,
        address2 = ad.address2,
        address3 = ad.address3,
        isDefault = ad.isDefault
      };
    }
    public static Address toAddress(this AddressDTO ad1)
    {
      return new Address()
      {
        id = ad1.id,
        customerId = ad1.customerId,
        street = ad1.street,
        address1 = ad1.address1,
        address2 = ad1.address2,
        address3 = ad1.address3,
        isDefault = ad1.isDefault
      };
    }
    public static void Mapto7(this Address ad, AddressDTO ad1)
    {
      ad.id = ad1.id;
      ad.customerId = ad1.customerId;
      ad.street = ad1.street;
      ad.address1 = ad1.address1;
      ad.address2 = ad1.address2;
      ad.address3 = ad1.address3;
      ad.isDefault = ad1.isDefault;
    }
    public static CategoryDTO toCategoryDTO(this Category c)
    {
      return new CategoryDTO()
      {
        id = c.id,
        name = c.name
      };
    }
    public static Category toCategory(this CategoryDTO c1)
    {
      return new Category()
      {
        id = c1.id,
        name = c1.name
      };
    }
    public static void Mapto8(this Category c, CategoryDTO c1)
    {
      c.id = c1.id;
      c.name = c1.name;
    }
    public static SellerDTO toSellerDTO(this Seller s)
    {
      return new SellerDTO()
      {
        id = s.id,
        phoneNumber = s.phoneNumber,
        name = s.name,
        email = s.email,
        password = s.password,
        secretKey = s.secretKey,
        accessToken = s.accessToken,
        accessExpire = s.accessExpire,
        refreshToken = s.refreshToken
      };
    }
    public static Seller toSeller(this SellerDTO s1)
    {
      return new Seller()
      {
        id = s1.id,
        phoneNumber = s1.phoneNumber,
        name = s1.name,
        email = s1.email,
        password = s1.password,
        secretKey = s1.secretKey,
        accessToken = s1.accessToken,
        accessExpire = s1.accessExpire,
        refreshToken = s1.refreshToken
      };
    }
    public static void Mapto9(this Seller s, SellerDTO s1)
    {
      s.id = s1.id;
      s.phoneNumber = s1.phoneNumber;
      s.name = s1.name;
      s.email = s1.email;
      s.password = s1.password;
      s.secretKey = s1.secretKey;
      s.accessToken = s1.accessToken;
      s.accessExpire = s1.accessExpire;
      s.refreshToken = s1.refreshToken;
    }
    public static SelleraddressDTO toSellerAddressDTO(this Selleraddress s)
    {
      return new SelleraddressDTO()
      {


        id = s.id,
        sellerId = s.sellerId,
        street = s.street,
        address1 = s.address1,
        address2 = s.address2,
        address3 = s.address3,
        isDefault = s.isDefault,
      };
    }
    public static Selleraddress toselleraddress(this SelleraddressDTO s1)
    {
      return new Selleraddress()
      {
        id = s1.id,
        sellerId = s1.sellerId,
        street = s1.street,
        address1 = s1.address1,
        address2 = s1.address2,
        address3 = s1.address3,
        isDefault = s1.isDefault
      };
    }
    public static void Mapto10(this Selleraddress s, SelleraddressDTO s1)
    {
      s.id = s1.id;
      s.sellerId = s1.sellerId;
      s.street = s1.street;
      s.address1 = s1.address1;
      s.address2 = s1.address2;
      s.address3 = s1.address3;
      s.isDefault = s1.isDefault;
    }
    public static CartDTO toCartDto(this Cart c)
    {
      return new CartDTO()
      {
        id = c.id,
        customerId = c.customerId,
        shippingFee = c.shippingFee,
        items = c.items,
        totalPrice = c.totalPrice
      };
    }
    public static Cart tocart(this CartDTO c1)
    {
      return new Cart()
      {
        id = c1.id,
        customerId = c1.customerId,
        shippingFee = c1.shippingFee,
        items = c1.items,
        totalPrice = c1.totalPrice
      };
    }
    public static void Mapto11(this Cart c, CartDTO c1)
    {
      c.id = c1.id;
      c.customerId = c1.customerId;
      c.shippingFee = c1.shippingFee;
      c.items = c1.items;
      c.totalPrice = c1.totalPrice;
    }
  }
}



