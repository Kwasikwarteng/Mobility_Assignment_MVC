using Microsoft.EntityFrameworkCore;

namespace Mobility_Assignment_MVC.Models.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
