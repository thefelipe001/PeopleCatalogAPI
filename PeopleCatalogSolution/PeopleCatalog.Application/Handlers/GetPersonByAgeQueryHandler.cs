using MediatR;
using PeopleCatalog.Application.DTOs;
using PeopleCatalog.Application.Queries;
using PeopleCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCatalog.Application.Handlers
{
    public class GetPersonByAgeQueryHandler : IRequestHandler<GetPersonByAgeQuery, List<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonByAgeQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<PersonDto>> Handle(GetPersonByAgeQuery request, CancellationToken cancellationToken)
        {
            // Llamar al repositorio para obtener las personas mayores a la edad especificada
            var people = await _personRepository.GetPeopleAboveAge(request.Age);

            // Convertir el resultado a DTOs o devolver directamente
            return people.Select(person => new PersonDto
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

            }).ToList();
        }
    }
}
