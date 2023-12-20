using Ecomerce.Models;

namespace Ecomerce.Repositories.interfaces
{
    public interface IShopingCartInterface: IGenericRepository<ShoppingCart>
    {
        public void Update (ShoppingCart shoppingCart);
        public ShoppingCart GetByIdWithInclude(int? id);
        public ShoppingCart GetByUserIDAndProductId(string UserId, int ProductId);
        public IEnumerable<ShoppingCart> GetAllByUserIdIncludeProducts(string UserId);


    }
}
