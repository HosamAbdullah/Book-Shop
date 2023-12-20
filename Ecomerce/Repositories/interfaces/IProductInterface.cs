using Ecomerce.Models;

namespace Ecomerce.Repositories.interfaces
{
    public interface IProductInterface:IGenericRepository<Product>
    {
        public void Update(Product product);
        public IEnumerable<Product> GetAllWithInclude();
        public Product GetByIdWithInclude(int? id);
    }
}
