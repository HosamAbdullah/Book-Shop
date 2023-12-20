using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;

namespace Ecomerce.Repositories.RepoClasses
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryInterface category { get; set; }
        public IProductInterface product { get; set; }
        public IShopingCartInterface shoppingCart { get; set; }
		public IOrderDetailsInterface orderDetails { get; set; }
		public IOrderInterface order { get; set; }

		private ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            category = new CategoryRepo(db);
            product = new ProductRepo(db);
            shoppingCart = new ShoppingCartRepo(db);
            orderDetails = new OrderDetailsRepo(db);
            order=new OrderRepo(db);
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
