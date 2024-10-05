using PeopleCatalog.Application.DTOs;
using PeopleCatalog.Application.Interfaces;
using PeopleCatalog.Domain.Entities;
using PeopleCatalog.Domain.Exceptions;
using PeopleCatalog.Domain.Interfaces;
using PeopleCatalog.Domain.ValueObjects;


namespace PeopleCatalog.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDto>> GetAllPeople()
        {
            var people = await _personRepository.GetAllAsync();
            return people.Select(person => new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                Street = person.Address.Street,
                City = person.Address.City,
                PostalCode = person.Address.PostalCode
            });
        }

        public async Task<PersonDto> GetPersonById(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                throw new PersonNotFoundException(id); // Lanzar excepción si no se encuentra la persona
            }

            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                Street = person.Address.Street,
                City = person.Address.City,
                PostalCode = person.Address.PostalCode
            };
        }


        public async Task<int> CreatePerson(PersonDto personDto)
        {
            if (string.IsNullOrEmpty(personDto.Street) || string.IsNullOrEmpty(personDto.City) || string.IsNullOrEmpty(personDto.PostalCode))
            {
                throw new ArgumentException("Address fields cannot be null or empty.");
            }

            var address = new Address(personDto.Street, personDto.City, personDto.PostalCode);

            var person = new Person(
                personDto.FirstName ?? "Default First Name",  
                personDto.LastName ?? "Default Last Name",   
                personDto.Email ?? "default@example.com",    
                personDto.DateOfBirth,  
                personDto.PhoneNumber ?? "0000000000",        
                address
            );

            await _personRepository.AddAsync(person);
            return person.Id;
        }


        public async Task<bool> UpdatePerson(int id, PersonDto personDto)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return false; 
            }

            if (string.IsNullOrEmpty(personDto.FirstName) || string.IsNullOrEmpty(personDto.LastName) ||
                string.IsNullOrEmpty(personDto.Email))
            {
                throw new ArgumentException("FirstName, LastName, Email  son campos obligatorios.");
            }


            if (string.IsNullOrEmpty(personDto.Street) || string.IsNullOrEmpty(personDto.City) || string.IsNullOrEmpty(personDto.PostalCode))
            {
                throw new ArgumentException("Street, City, y PostalCode son campos obligatorios de la dirección.");
            }

            person.FirstName = personDto.FirstName;
            person.LastName = personDto.LastName;
            person.Email = personDto.Email;
            person.DateOfBirth = personDto.DateOfBirth; 
            person.PhoneNumber = personDto.PhoneNumber ?? person.PhoneNumber; 

            person.Address = new Address(
                personDto.Street ?? person.Address.Street,     
                personDto.City ?? person.Address.City,         
                personDto.PostalCode ?? person.Address.PostalCode 
            );

            await _personRepository.UpdateAsync(person);
            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return false;
            }

            await _personRepository.DeleteAsync(person);
            return true;
        }
    }
}
