using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;

namespace Ecomerce.Repositories.RepoClasses
{
    public class CategoryRepo:GenericRepo<Category>, ICategoryInterface
	{
        private readonly ApplicationDbContext db;

        public CategoryRepo(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }

       

        public void Update(Category category)
        {
            db.Categories.Update(category);
        }
    }
}
