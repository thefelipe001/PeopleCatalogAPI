using MediatR;
using PeopleCatalog.Application.DTOs;

namespace PeopleCatalog.Application.Queries
{
    public class GetPersonByIdQuery : IRequest<PersonDto>
    {
        public int Id { get; }

        public GetPersonByIdQuery(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            Id = id;
        }
    }
}



