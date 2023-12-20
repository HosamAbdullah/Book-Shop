using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.Repositories.RepoClasses
{
    public class ProductRepo:GenericRepo<Product>,IProductInterface
    {
        private ApplicationDbContext db;
        public ProductRepo(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public IEnumerable<Product> GetAllWithInclude()
        {
            return (db.Products.Include(p=>p.category).ToList());
        }
        public Product GetByIdWithInclude( int? id)
        {
            return db.Products.Include(p => p.category).Where(p=>p.Id==id).FirstOrDefault();
        }
        public void Update(Product product)
        {
            db.Products.Update(product);
        }

    }
}
