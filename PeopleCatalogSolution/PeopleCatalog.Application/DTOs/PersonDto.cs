
namespace PeopleCatalog.Application.DTOs
{
    public class PersonDto
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }     
        public string? LastName { get; set; }       
        public string? Email { get; set; }          
        public DateTime DateOfBirth { get; set; }  
        public string? PhoneNumber { get; set; }   
        public string? Street { get; set; }         
        public string? City { get; set; }           
        public string? PostalCode { get; set; }    
    }


}
