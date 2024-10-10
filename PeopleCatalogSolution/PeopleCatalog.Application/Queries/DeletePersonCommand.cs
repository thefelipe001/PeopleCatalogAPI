using MediatR;

namespace PeopleCatalog.Application.Commands
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeletePersonCommand(int id)
        {
            Id = id;
        }
    }
}
