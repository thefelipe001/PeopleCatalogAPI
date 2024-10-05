using MediatR;
using PeopleCatalog.Application.DTOs;
using PeopleCatalog.Application.Queries;
using PeopleCatalog.Domain.Interfaces;


namespace PeopleCatalog.Application.Handlers
{
    public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPeopleQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDto>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetAllAsync();
            return people.Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                PhoneNumber = p.PhoneNumber,
                Street = p.Address.Street,
                City = p.Address.City,
                PostalCode = p.Address.PostalCode
            });
        }
    }
}
