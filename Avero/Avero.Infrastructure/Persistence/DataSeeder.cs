using Avero.Core.Entities;
using Avero.Infrastructure.Persistence.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static void seed(ApplicationDBContext context, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (!roleManager.RoleExistsAsync("WholeSealer").Result)
            {
                roleManager.CreateAsync(new IdentityRole{Name = "WholeSealer"});
                roleManager.CreateAsync(new IdentityRole{Name = "Retailer" });
            }

            if (!context.city.Any())
            {
                var cities = new City[]
                {
                    new City{name = "Damascus"},
                    new City{name = "Rif Dimashq"},
                    new City{name = "Aleppo"},
                    new City{name = "Idlib"},
                    new City{name = "Al-Raqqah"},
                    new City{name = "Latakia"},
                    new City{name = "Al-Hasakah"},
                    new City{name = "Tartus"},
                    new City{name = "Hama"},
                    new City{name = "Deir ez-Zor"},
                    new City{name = "Homs"},
                    new City{name = "Quneitra"},
                    new City{name = "Daraa"},
                    new City{name = "As-Suwayda"},
                };
                foreach (City c in cities)
                {
                    context.city.Add(c);
                }
                context.SaveChanges();
            }

            if (!context.neighborhood.Any())
            {
                var damas = context.city.First(x => x.name == "Damascus");
                var alepp = context.city.First(x => x.name == "Aleppo");

                var neighborhoods = new Neighborhood[]
                {
                    new Neighborhood{name = "domar", city = damas},
                    new Neighborhood{name = "al hama", city = damas},
                    new Neighborhood{name = "duma", city = damas},
                    new Neighborhood{name = "feed", city = alepp},
                    new Neighborhood{name = "malaab", city = alepp},
                };
                foreach (Neighborhood n in neighborhoods)
                {
                    context.neighborhood.Add(n);
                }
                context.SaveChanges();
            }

            if (!context.catagory.Any())
            {

                var catagories = new Catagory[]
                {
                    new Catagory{name = "Sport"},
                    new Catagory{name = "Food"},
                    new Catagory{name = "Phone"},
                    new Catagory{name = "Other"},
                };
                foreach (Catagory n in catagories)
                {
                    context.catagory.Add(n);
                }
                context.SaveChanges();
            }


        }


    }
}
