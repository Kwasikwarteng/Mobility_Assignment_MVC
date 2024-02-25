using Microsoft.EntityFrameworkCore;
using Mobility_Assignment_MVC.Models;
using Mobility_Assignment_MVC.Models.Data;
using WebService.Services.IServices;

namespace WebService.Services
{
    public class PersonService : IPersonService
    {
        private readonly WebServiceDbContext _dbContext;
        public PersonService(WebServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get All
        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            var people = await _dbContext.Persons.ToListAsync();
            return people;
        }

        //Add
        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _dbContext.Persons.FindAsync(id);
            return person;
        }


        //Delete
        public async Task DeleteAsync(int id)
        {
            var person = await _dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                _dbContext.Persons.Remove(person);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Person person)
        {
            try
            {
                var personItem = _dbContext.Persons.Add(person);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw;
            }
        }

        public async Task<Person> SearchAsync(string firstName, string lastName)
        {
            var person = await _dbContext.Persons.FirstOrDefaultAsync(x => x.First_Name == firstName && x.Last_Name == lastName);
            return person;
        }
    }
}
