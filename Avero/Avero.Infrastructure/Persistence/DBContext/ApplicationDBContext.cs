using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avero.Application.Interfaces;
using Avero.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Avero.Infrastructure.Persistence.DBContext
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }


        public DbSet<Catagory>? catagory { get; set; }
        public DbSet<City>? city { get; set; }
        public DbSet<Neighborhood>? neighborhood { get; set; }
        public DbSet<Order>? order { get; set; }
        public DbSet<Order_details>? order_details { get; set; }
        public DbSet<Product>? product { get; set; }
        public DbSet<Product_review>? product_review { get; set; }

    }
}
