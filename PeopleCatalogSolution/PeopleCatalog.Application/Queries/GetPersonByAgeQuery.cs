using MediatR;
using PeopleCatalog.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCatalog.Application.Queries
{
    public class GetPersonByAgeQuery : IRequest<List<PersonDto>>
    {
        public int Age { get; }

        public GetPersonByAgeQuery(int age)
        {
            Age = age;
        }
    }
}
