using MultiShop.Cargo.DataAccessLayer.Abstruct;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly CargoContext _context;

        public GenericRepository(CargoContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            if (value != null)
            {
                _context.Set<T>().Remove(value);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
        }

        public List<T> GetListAll()
        {
            var values = _context.Set<T>().ToList();
            return values;
        }

        public T GetById(int id)
        {
            var value = _context.Set<T>().Find(id);
            return value!;
        }

        public void Update(T entity)
        {
            var values = _context.Set<T>().Update(entity);
            if (values != null)
            {
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Update operation failed. Entity not found.");

            }
        }
    }
}
