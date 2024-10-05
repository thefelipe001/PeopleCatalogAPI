using MediatR;
using PeopleCatalog.Application.Commands;
using PeopleCatalog.Domain.Entities;
using PeopleCatalog.Domain.Interfaces;
using PeopleCatalog.Domain.ValueObjects;


namespace PeopleCatalog.Application.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // Proveer valores predeterminados si alguno es null
            var firstName = request.FirstName ?? "Unknown FirstName";
            var lastName = request.LastName ?? "Unknown LastName";
            var email = request.Email ?? "unknown@example.com";
            var dateOfBirth = request.DateOfBirth == default ? DateTime.Now : request.DateOfBirth;
            var phoneNumber = request.PhoneNumber ?? "000-000-0000";

            var street = request.Street ?? "Unknown Street";
            var city = request.City ?? "Unknown City";
            var postalCode = request.PostalCode ?? "00000";

            // Crear el objeto Address (Objeto de Valor)
            var address = new Address(street, city, postalCode);

            // Crear el objeto Person (Entidad)
            var person = new Person(
                firstName,
                lastName,
                email,
                dateOfBirth,
                phoneNumber,
                address
            );

            // Agregar la persona al repositorio
            await _personRepository.AddAsync(person);

            // Retornar el ID de la persona recién creada
            return person.Id;
        }


    }
}
