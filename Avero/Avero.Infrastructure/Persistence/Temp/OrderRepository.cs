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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Order Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Order Insert(Order obj)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }

}
