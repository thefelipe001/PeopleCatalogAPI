using PeopleCatalog.Domain.ValueObjects;

namespace PeopleCatalog.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }

        public Person(string firstName, string lastName, string email, DateTime dateOfBirth, string phoneNumber, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        // Si necesitas un constructor vacío para Entity Framework
        public Person() { }
    }

}
