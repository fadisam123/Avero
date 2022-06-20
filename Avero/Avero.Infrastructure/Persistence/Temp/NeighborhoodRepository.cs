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
    public class NeighborhoodRepository : INeighborhoodRepository
    {
        private readonly ApplicationDBContext _context;

        public NeighborhoodRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Neighborhood Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Neighborhood> GetAll()
        {
            throw new NotImplementedException();
        }

        public Neighborhood GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Neighborhood Insert(Neighborhood obj)
        {
            throw new NotImplementedException();
        }

        public Neighborhood Update(Neighborhood obj)
        {
            throw new NotImplementedException();
        }
    }
}
