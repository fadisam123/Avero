using Avero.Application.Interfaces;
using Avero.Core.Shared;
using Avero.Infrastructure.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _context;
        private DbSet<T> entities;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public T Delete(int id)
        {
            var obj = entities.SingleOrDefault(s => s.Id == id);
            if (obj == null)
            {
                throw new KeyNotFoundException("id");
            }
            entities.Remove(obj);
            _context.SaveChanges();
            return obj;
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public T GetById(int id)
        {
            var obj = entities.SingleOrDefault(s => s.Id == id);
            if (obj == null)
            {
                throw new KeyNotFoundException("id");
            }
            return obj;
        }

        public T Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            entities.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public T Update(T objToChange)
        {
            if (objToChange == null)
            {
                throw new ArgumentNullException("obj");
            }

            var obj = entities.Attach(objToChange);
            obj.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return objToChange;
        }
    }
}
