using MediatR;
using PeopleCatalog.Application.DTOs;

namespace PeopleCatalog.Application.Queries
{
    public class GetPersonByIdQuery : IRequest<PersonDto>
    {
        public int Id { get; set; }

        public GetPersonByIdQuery(int id)
        {
            Id = id;
        }
    }
}
