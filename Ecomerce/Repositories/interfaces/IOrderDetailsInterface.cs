using Ecomerce.Models;

namespace Ecomerce.Repositories.interfaces
{
    public interface IOrderDetailsInterface: IGenericRepository<OrderDetails>
    {
        public void Update (OrderDetails orderDetails);
        public IEnumerable<OrderDetails> GetAllById(int id);


    }
}
