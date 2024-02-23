using Microsoft.EntityFrameworkCore;

namespace Mobility_Assignment_MVC.Models.Data
{
    public class WebServiceDbContext : DbContext
    {

        public WebServiceDbContext(DbContextOptions<WebServiceDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
