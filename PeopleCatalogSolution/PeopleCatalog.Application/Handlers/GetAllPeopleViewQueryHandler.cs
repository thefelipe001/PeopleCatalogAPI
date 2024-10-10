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
    public class GetAllPeopleViewQueryHandler : IRequestHandler<GetAllPeopleViewQuery, IEnumerable<PersonViewDto>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPeopleViewQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonViewDto>> Handle(GetAllPeopleViewQuery request, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetPersonSummariesAsync();
            return people.Select(p => new PersonViewDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
               
            });
        }
    }
}
