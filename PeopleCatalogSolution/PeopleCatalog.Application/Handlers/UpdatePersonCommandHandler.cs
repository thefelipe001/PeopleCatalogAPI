using MediatR;
using PeopleCatalog.Application.Commands;
using PeopleCatalog.Domain.Exceptions;
using PeopleCatalog.Domain.Interfaces;
using PeopleCatalog.Domain.ValueObjects;


namespace PeopleCatalog.Application.Handlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentException("El ID no puede ser nulo.");
            }

            var person = await _personRepository.GetByIdAsync(request.Id.Value);

            // Verificar si la persona existe
            if (person == null)
            {
                throw new PersonNotFoundException(request.Id.Value);
            }

          
            var address = new Address(
                request.Street ?? "Unknown Street",   
                request.City ?? "Unknown City",       
                request.PostalCode ?? "00000");      

            person.FirstName = request.FirstName ?? "Unknown FirstName";  
            person.LastName = request.LastName ?? "Unknown LastName";     
            person.Email = request.Email ?? "unknown@example.com";       
            person.DateOfBirth = request.DateOfBirth;                      
            person.PhoneNumber = request.PhoneNumber ?? "000-000-0000";    
            person.Address = address;                                      

            // Actualizar la persona en el repositorio
            await _personRepository.UpdateAsync(person);

            return true;
        }

    }
}
