using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        private readonly DataContext _context;

        protected RepositoryBase(DataContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}