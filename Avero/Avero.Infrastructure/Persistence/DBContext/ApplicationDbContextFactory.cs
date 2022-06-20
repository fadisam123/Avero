using Avero.Infrastructure.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
{
    public ApplicationDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
        optionsBuilder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=Avero;Integrated Security=True");

        return new ApplicationDBContext(optionsBuilder.Options);
    }
}