using Mobility_Assignment_MVC.Models;

namespace WebService.Services.Interfaces
{
    public interface IWebService
    {
        Task AddAsync(Person person);
        Task DeleteAsync(int id);
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task SearchAsync(string name);
    }
}
