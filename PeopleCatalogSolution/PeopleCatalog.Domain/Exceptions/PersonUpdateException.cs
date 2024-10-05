using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCatalog.Domain.Exceptions
{
    public class PersonUpdateException : Exception
    {
        public PersonUpdateException(int id)
            : base($"Ocurrió un error al actualizar la persona con ID {id}.")
        {
        }

        public PersonUpdateException(int id, string message)
            : base($"Error al actualizar la persona con ID {id}: {message}")
        {
        }
    }

}
