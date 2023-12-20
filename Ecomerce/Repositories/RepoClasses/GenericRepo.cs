using Ecomerce.Models;
using Ecomerce.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecomerce.Repositories.RepoClasses
{
    public class GenericRepo<T>:IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet; 

        public GenericRepo(ApplicationDbContext Db) 
        {
            db = Db;
            this.dbSet=db.Set<T>();
            
        }

        public void add(T entity)
        {
           dbSet.Add(entity);
        }

        public T get(Expression<Func<T, bool>> filter)
        {
           // IQueryable<T> query = dbSet;
           //return query.Where(filter).FirstOrDefault();
          return dbSet.Where(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T getById(int? id)
        {
            return dbSet.Find(id);
        }

        public void remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void removeRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
