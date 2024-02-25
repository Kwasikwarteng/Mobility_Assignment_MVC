using Mobility_Assignment_MVC.Models;

namespace WebService.Services.IServices
{
    public interface IPersonService
    {
        Task AddAsync(Person person);
        Task DeleteAsync(int id);
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task SearchAsync(string name);
        Task<Person> SearchAsync(string firstName, string lastName);
    }
}
