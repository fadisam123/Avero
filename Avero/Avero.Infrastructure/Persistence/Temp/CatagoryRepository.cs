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
    public class CatagoryRepository : ICatagoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CatagoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Catagory Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Catagory> GetAll()
        {
            throw new NotImplementedException();
        }

        public Catagory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Catagory Insert(Catagory obj)
        {
            throw new NotImplementedException();
        }

        public Catagory Update(Catagory obj)
        {
            throw new NotImplementedException();
        }
    }
}
