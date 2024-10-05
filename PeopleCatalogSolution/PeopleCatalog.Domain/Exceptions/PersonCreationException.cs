using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCatalog.Domain.Exceptions
{
    public class PersonCreationException : Exception
    {
        public PersonCreationException(string message)
            : base($"Error al crear la persona: {message}")
        {
        }

        public PersonCreationException()
            : base("Error inesperado al crear la persona.")
        {
        }
    }
}
