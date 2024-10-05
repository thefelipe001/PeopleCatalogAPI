using MediatR;
using PeopleCatalog.Application.DTOs;

namespace PeopleCatalog.Application.Queries
{
    public class GetAllPeopleQuery : IRequest<IEnumerable<PersonDto>>
    {
    }
}