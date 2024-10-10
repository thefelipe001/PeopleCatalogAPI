using MediatR;
using PeopleCatalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCatalog.Application.Queries
{
    public class GetAllPeopleViewQuery : IRequest<IEnumerable<PersonViewDto>>
    {
    }
}
