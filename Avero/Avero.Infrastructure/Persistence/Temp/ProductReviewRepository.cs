using Avero.Application.Interfaces;
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
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public product_review Delete(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var obj = _context.product_review.Find(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (obj == null)
            {
                throw new KeyNotFoundException();
            }
            _context.product_review.Remove(obj);
            _context.SaveChanges();
            return obj;
        }

        public IEnumerable<product_review> GetAll()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.product_review;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public product_review GetById(int id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var obj = _context.product_review.Find(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (obj == null)
            {
                throw new KeyNotFoundException();
            }
            return obj;
        }

        public product_review Insert(product_review obj)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _context.product_review.Add(obj);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            _context.SaveChanges();
            return obj;
        }

        public product_review Update(product_review objToChange)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var obj = _context.product_review.Attach(objToChange);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            obj.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return objToChange;
        }
    }
}
