using Microsoft.EntityFrameworkCore;
using Mobility_Assignment_MVC.Models;
using Mobility_Assignment_MVC.Models.Data;
using WebService.Services.Interfaces;

namespace WebService.Services
{
    public class WebService : IWebService
    {
        private readonly WebServiceDbContext _dbContext;
        public WebService(WebServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //List
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
                var budgetItem = _dbContext.Persons.Add(person);

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw;
            }
        }

        public async Task SearchAsync(string name)
        {

        }
    }
}
