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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDBContext _context;

        public CityRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public City Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> GetAll()
        {
            throw new NotImplementedException();
        }

        public City GetById(int id)
        {
            throw new NotImplementedException();
        }

        public City Insert(City obj)
        {
            throw new NotImplementedException();
        }

        public City Update(City obj)
        {
            throw new NotImplementedException();
        }
    }
}
