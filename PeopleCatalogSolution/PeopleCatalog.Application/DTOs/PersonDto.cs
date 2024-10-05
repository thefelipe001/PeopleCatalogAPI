
namespace PeopleCatalog.Application.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }      // Obligatorio, no nullable
        public string? LastName { get; set; }       // Obligatorio, no nullable
        public string? Email { get; set; }          // Obligatorio, no nullable
        public DateTime DateOfBirth { get; set; }  // Obligatorio, no nullable
        public string? PhoneNumber { get; set; }   // Nullable, puede ser nulo
        public string? Street { get; set; }         // Obligatorio, no nullable
        public string? City { get; set; }           // Obligatorio, no nullable
        public string? PostalCode { get; set; }    // Nullable, puede ser nulo
    }


}
