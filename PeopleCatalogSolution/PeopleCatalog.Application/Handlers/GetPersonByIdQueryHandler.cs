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
            _personRepository = personRepository;
        }

        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);

            if (person == null)
            {
                throw new PersonNotFoundException(request.Id);
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
    }
}
