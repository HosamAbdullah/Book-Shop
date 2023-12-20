using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.Repositories.RepoClasses
{
    public class OrderDetailsRepo:GenericRepo<OrderDetails>,IOrderDetailsInterface
    {
        private readonly ApplicationDbContext db;

        public OrderDetailsRepo(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }

       

        public void Update(OrderDetails orderDetails)
        {
            db.orderDetails.Update(orderDetails);
        }
        public IEnumerable<OrderDetails>GetAllById(int id)
        {
           return db.orderDetails.Where(o=>o.Id==id).Include(o=>o.product);
        }
    }
}
