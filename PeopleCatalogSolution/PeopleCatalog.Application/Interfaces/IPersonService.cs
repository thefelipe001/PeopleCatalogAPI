using PeopleCatalog.Application.DTOs;

namespace PeopleCatalog.Application.Interfaces
{
    public interface IPersonService
    {
        // Obtener todas las personas
        Task<IEnumerable<PersonDto>> GetAllPeople();

        // Obtener una persona por su ID
        Task<PersonDto> GetPersonById(int id);

        // Crear una nueva persona
        Task<int> CreatePerson(PersonDto personDto);

        // Actualizar una persona existente
        Task<bool> UpdatePerson(int id, PersonDto personDto);

        // Eliminar una persona
        Task<bool> DeletePerson(int id);

    }
}
