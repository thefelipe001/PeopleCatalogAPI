using PeopleCatalog.Domain.Entities;
using PeopleCatalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using PeopleCatalog.Infrastructure.Data;

namespace PeopleCatalog.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todas las personas
        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        // Obtener una persona por su ID
        public async Task<Person?> GetByIdAsync(int? id)
        {
            return await _context.People.FindAsync(id);
        }


        // Añadir una nueva persona
        public async Task AddAsync(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        // Actualizar una persona existente
        public async Task UpdateAsync(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        // Eliminar una persona
        public async Task DeleteAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetPeopleAboveAge(int ageLimit)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL GetPeopleAboveOrEqualAge({0})", ageLimit);

            // Query de tabla temporal
            return await _context.People
                .FromSqlRaw("SELECT * FROM PersistentPeople")
                .ToListAsync();
        }



        // Consultar la vista PersonSummary
        public async Task<List<PersonSummary>> GetPersonSummariesAsync()
        {
            return await _context.Set<PersonSummary>().ToListAsync();
        }
    }
}
