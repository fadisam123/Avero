using Avero.Application.Interfaces.Temp;
using Avero.Core.Entities;
using Avero.Infrastructure.Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Infrastructure.Persistence.Temp
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Product Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product Insert(Product obj)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
