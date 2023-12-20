using Ecomerce.Models;

namespace Ecomerce.Repositories.interfaces
{
    public interface IOrderInterface : IGenericRepository<Order>
    {
        public void Update (Order order);
      
    }
}
