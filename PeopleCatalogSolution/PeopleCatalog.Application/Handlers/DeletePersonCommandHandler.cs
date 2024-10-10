using MediatR;
using PeopleCatalog.Application.Commands;
using PeopleCatalog.Domain.Interfaces;

namespace PeopleCatalog.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
            {
                return false; // Retornar false si no se encuentra la persona
            }

            await _personRepository.DeleteAsync(person);
            return true; // Retornar true si la eliminación fue exitosa
        }
    }
}
