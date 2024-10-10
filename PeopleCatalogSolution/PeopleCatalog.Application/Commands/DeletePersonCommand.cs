using MediatR;

namespace PeopleCatalog.Application.Commands
{
    public class DeletePersonCommand : IRequest<bool>// El tipo de retorno un bool
    {
        public int Id { get; set; }

        public DeletePersonCommand(int id)
        {
            Id = id;
        }
    }
}
