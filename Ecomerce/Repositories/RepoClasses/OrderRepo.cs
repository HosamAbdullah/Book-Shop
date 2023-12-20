using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;

namespace Ecomerce.Repositories.RepoClasses
{
    public class OrderRepo:GenericRepo<Order>,IOrderInterface
    {
        private readonly ApplicationDbContext db;

        public OrderRepo(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }

       

        public void Update(Order order)
        {
            db.orders.Update(order);
        }
    }
}
