using System.Linq;
using Ardalis.Specification;
using Models;

namespace apiproject.Specifications
{
  public class OrderWithDetailsSpecification : Specification<Order>
    {
        public OrderWithDetailsSpecification(){
            Query
            .Include(o => o.orderItems);
        }

        public OrderWithDetailsSpecification(int id){
            Query
            .Where(o => o.id == id)
            .Include(o => o.orderItems)
            .Include(o => o.customer);
        }
    }
}