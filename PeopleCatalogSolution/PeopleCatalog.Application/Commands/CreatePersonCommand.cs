using MediatR;
using System;

namespace PeopleCatalog.Application.Commands
{
    public class CreatePersonCommand : IRequest<int> // El tipo de retorno es el ID de la nueva persona
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}
