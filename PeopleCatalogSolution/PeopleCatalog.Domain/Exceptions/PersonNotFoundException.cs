
namespace PeopleCatalog.Domain.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(int id)
            : base($"La persona con ID {id} no fue encontrada.")
        {
        }

        public PersonNotFoundException(string message)
            : base(message)
        {
        }
    }
}
