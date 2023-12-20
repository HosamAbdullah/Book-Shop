using System.Linq.Expressions;

namespace Ecomerce.Repositories.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T getById(int? id);
        T get (Expression<Func<T, bool>> filter);
        void add (T entity);
        void remove (T entity);
        void removeRange (IEnumerable<T> entities);

    }
}
