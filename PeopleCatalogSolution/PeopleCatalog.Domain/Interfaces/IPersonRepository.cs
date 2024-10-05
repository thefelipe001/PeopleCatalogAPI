using PeopleCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleCatalog.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();

        Task<Person?> GetByIdAsync(int? id);

        Task AddAsync(Person person);

        Task UpdateAsync(Person person);

        Task DeleteAsync(Person person);

        Task<List<Person>> GetPeopleAboveAge(int ageLimit);

        Task<List<PersonSummary>> GetPersonSummariesAsync();
    }
}
