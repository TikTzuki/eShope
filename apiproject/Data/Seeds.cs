using System;
using System.Linq;
using DTO;
using System.Collections.Generic;
using Data;
using Models;
using apiproject.Models.OrderAggregate;

namespace Data
{
  public class Seeds
  {
    public static void Seed(EcommerContext context)
    {
//       context.Database.EnsureCreated();

//       if (!context.brands.Any())
//       {
//         List<brand> b1 = new List<brand>()
//               {
//                    new brand{
//                        id=1,
//                        name="number1"
//                    },
//                    new brand{
//                        id=2,
//                        name="number2"
//                    },
//                    new brand{
//                        id=3,
//                        name="number3"
//                    }
//               };
//         context.brands.AddRange(b1);
//         context.SaveChanges();
//       }
//       if (!context.product.Any())
//       {
//         List<Products> p1 = new List<Products>()
//                 {  new Products
//                 {
//                     id=1,
//                     categoryId=1,
//                     brandId=1,
//                     productName="New phone",
//                     description="newest product",
//                     status="yes"


//                 },
//                  new Products
//                 {
//                     id=2,
//                     categoryId=1,
//                     brandId=2,
//                     productName="New phone1",
//                     description="newest product1",
//                     status="yes"

//                     }
//                 };
//         context.product.AddRange(p1);
//         context.SaveChanges();
//       }
//       if (!context.skus.Any())
//       {
//         context.skus.AddRange(new List<Sku>()
//                 {
//                   new Sku{

//                       sellerSku="Nguyen van A",
//                       available=1,
//                       quantity=100,
//                       color="Đỏ",
//                        size=15,
//                        height=20,
//                        width=20*15,
//                        length=200,
//                        weight=30,
//                        price=35M,
//                        productId=2
//                   },
//                   new Sku{
//                       sellerSku="Nguyen van B",
//                       available=1,
//                       quantity=100,
//                       color="Xanh",
//                       size=15,
//                       height=20,
//                       width=20*15,
//                        length=200,
//                        weight=30,
//                        price=20M,
//                        productId=1
//                   }
//                 }

//         );
//         context.SaveChanges();

//       }
//       if (!context.images.Any())
//       {
//         List<images> i1 = new List<images>()
//             {
//                 new images
//                 {
//                     url="/bitmap/vains.jpg",
//                     skuId=1,
//                     id=1
//                 },
//                 new images
//                 {
//                     url="/bitmap/bitis.jpg",
//                     skuId=2,
//                     id=2
//                 }
//             };
//         context.images.AddRange(i1);
//         context.SaveChanges();
//       }
//       if (!context.ordertable.Any())
//       {
//         List<Order> i2 = new List<Order>()
//               {
//                   new Order
//                   {
//                       id=1,
//                       createDate=new DateTime(2020,12,30,12,30,50),
//                       updateDate=new DateTime(2020,12,31,12,30,50),
//                       paymentMethod="credit card",
//                      shippingFee=2500,
//                      shippingAddress="12/5,ly tu trong",
//                      totalPrice=120000,
//                      status=EOrderStatus.unpaid
//                   },
//                   new Order
//                   {
//                       id=2,
//                       createDate=new DateTime(2020,12,31,12,30,50),
//                       updateDate=new DateTime(2020,10,30,12,30,50),
//                       paymentMethod="directly",
//                      shippingFee=2500,
//                      shippingAddress="12/5,ly tu trong",
//                      totalPrice=120000,
//                      status=EOrderStatus.unpaid
//                   }

//               };
//         context.ordertable.AddRange(i2);
//         context.SaveChanges();
//       }
//     }
//     public static Products product1;
//     private static Dictionary<string, Category> categories;
//     public static brand brand1;
//     public static Dictionary<string, Category> Categories
//     {
//       get
//       {
//         if (categories == null)
//         {
//           var genrelist = new Category[]
//           {
//                        new Category{
//                            id=1,
//                            name="hello long tran"
//                        }
//                        ,new Category{
//                            id=2,
//                            name="fuck off"
//                        }
//           };
//           categories = new Dictionary<string, Category>();
//           foreach (var genre in genrelist)
//           {
//             categories.Add(genre.name, genre);
//           }

//         }
//         return categories;

//       }



    }
  }
}

