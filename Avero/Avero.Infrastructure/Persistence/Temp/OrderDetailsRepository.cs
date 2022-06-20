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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderDetailsRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Order_details Delete(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var obj = _context.order_details.Find(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (obj == null)
            {
                throw new KeyNotFoundException();
            }
            _context.order_details.Remove(obj);
            _context.SaveChanges();
            return obj;
        }

        public IEnumerable<Order_details> GetAll()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.order_details;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Order_details GetById(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var obj = _context.order_details.Find(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (obj == null)
            {
                throw new KeyNotFoundException();
            }
            return obj;
        }

        public Order_details Insert(Order_details obj)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _context.order_details.Add(obj);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            _context.SaveChanges();
            return obj;
        }

        public Order_details Update(Order_details objToChange)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var obj = _context.order_details.Attach(objToChange);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            obj.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return objToChange;
        }
    }
}
