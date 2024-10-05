using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCatalog.Domain.Exceptions
{
    public class PersonDeletionException : Exception
    {
        public PersonDeletionException(int id)
            : base($"No se puede eliminar la persona con ID {id}.")
        {
        }

        public PersonDeletionException(int id, string reason)
            : base($"No se puede eliminar la persona con ID {id}: {reason}.")
        {
        }
    }

}
