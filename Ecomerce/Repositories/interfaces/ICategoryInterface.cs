using Ecomerce.Models;

namespace Ecomerce.Repositories.interfaces
{
    public interface ICategoryInterface:IGenericRepository<Category>
    {
        public void Update (Category category);
      
    }
}
