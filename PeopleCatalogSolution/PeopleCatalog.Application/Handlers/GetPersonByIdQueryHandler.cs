using MediatR;
using PeopleCatalog.Application.DTOs;
using PeopleCatalog.Application.Queries;
using PeopleCatalog.Domain.Exceptions;
using PeopleCatalog.Domain.Interfaces;

namespace PeopleCatalog.Application.Handlers
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonByIdQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            // Validar si el ID proporcionado es válido
            if (request.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(request.Id));
            }

            // Obtener la persona por ID
            var person = await _personRepository.GetByIdAsync(request.Id);

            // Si no se encuentra la persona, lanzar una excepción personalizada
            if (person == null)
            {
                throw new PersonNotFoundException(request.Id);
            }

            // Mapear la entidad Person a PersonDto
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                Street = person.Address?.Street ?? "N/A",
                City = person.Address?.City ?? "N/A",
                PostalCode = person.Address?.PostalCode ?? "N/A"
            };
        }
    }
}
