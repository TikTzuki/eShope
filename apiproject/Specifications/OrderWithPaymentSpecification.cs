using System.Linq;
using Ardalis.Specification;
using Models;

namespace apiproject.Specifications
{
    public class OrderWithPaymentSpecification:Specification<Order>
    {
       public OrderWithPaymentSpecification(string paymentIntentId){
           Query
           .Where(o => o.PaymentIntent.PaymentIntentId == paymentIntentId); 
       } 
    }
}