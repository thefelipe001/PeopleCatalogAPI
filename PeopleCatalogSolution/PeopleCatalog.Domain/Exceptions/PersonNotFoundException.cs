
namespace PeopleCatalog.Domain.Exceptions
{
    public class PersonNotFoundException : Exception
    {
      
        public PersonNotFoundException(int id)
            : base($"La persona con ID {id} no fue encontrada.")
        {
        }
        public PersonNotFoundException(int id,int age)
         : base($"La persona con edad {age} no fue encontrada.")
        {
        }
        public PersonNotFoundException(string message)
            : base(message)
        {
        }
    }
}
