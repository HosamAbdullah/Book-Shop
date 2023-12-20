using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.Repositories.RepoClasses
{
    public class ShoppingCartRepo : GenericRepo<ShoppingCart>,IShopingCartInterface
    {
        private readonly ApplicationDbContext db;

        public ShoppingCartRepo(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public ShoppingCart GetByIdWithInclude(int? id)
        {
            return db.ShoppingCarts.Include(s => s.product).Include(s=>s.user).Where(s=> s.Id == id).FirstOrDefault();
        }
        public ShoppingCart GetByUserIDAndProductId(string UserId,int ProductId)
        {
            return db.ShoppingCarts.Where(s=>s.UserId== UserId && s.ProductId== ProductId).FirstOrDefault();
        }
        public IEnumerable<ShoppingCart> GetAllByUserIdIncludeProducts(string UserId)
        {
            return db.ShoppingCarts.Where(s => s.UserId == UserId).Include(s=>s.product).ToList();
        }

        public void Update(ShoppingCart shoppingCart)
        {
            db.ShoppingCarts.Update(shoppingCart);
        }
    }
}
