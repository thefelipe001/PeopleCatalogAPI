namespace PeopleCatalog.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; } // Calle
        public string City { get; private set; } // Ciudad
        public string PostalCode { get; private set; } // Código postal

        public Address(string street, string city, string postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }

        // Comparación de igualdad para objetos de valor
        public override bool Equals(object? obj)
        {
            if (obj is Address other)
            {
                return Street == other.Street && City == other.City && PostalCode == other.PostalCode;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, PostalCode);
        }
    }
}
