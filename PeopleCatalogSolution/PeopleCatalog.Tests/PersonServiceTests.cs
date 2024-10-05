using Moq;
using PeopleCatalog.Application.DTOs;
using PeopleCatalog.Application.Interfaces;
using Xunit;

public class PersonServiceTests
{
    private readonly Mock<IPersonService> _mockPersonService;

    public PersonServiceTests()
    {
        _mockPersonService = new Mock<IPersonService>();
    }

    [Fact]
    public async Task CreatePerson_Should_ReturnPersonId()
    {
        // Arrange
        var personDto = new PersonDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            DateOfBirth = DateTime.Now,
            PhoneNumber = "123456789",
            Street = "123 Main St",
            City = "Somewhere",
            PostalCode = "12345"
        };

        // Configurar el servicio mock para que devuelva un ID cuando se cree una persona
        _mockPersonService.Setup(service => service.CreatePerson(It.IsAny<PersonDto>()))
            .ReturnsAsync(1);

        // Act
        var result = await _mockPersonService.Object.CreatePerson(personDto);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetPersonById_Should_ReturnPerson()
    {
        // Arrange
        var personDto = new PersonDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            DateOfBirth = DateTime.Now,
            PhoneNumber = "123456789",
            Street = "123 Main St",
            City = "Somewhere",
            PostalCode = "12345"
        };

        _mockPersonService.Setup(service => service.GetPersonById(1))
            .ReturnsAsync(personDto);

        // Act
        var result = await _mockPersonService.Object.GetPersonById(1);

        // Assert
        Assert.Equal(personDto, result);
    }
}
