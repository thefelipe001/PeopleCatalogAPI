using Moq;
using PeopleCatalog.Domain.Entities;
using PeopleCatalog.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class PersonRepositoryTests
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;

    public PersonRepositoryTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
    }

    [Fact]
    public async Task GetAllPeople_Should_ReturnListOfPeople()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com" }
        };

        _mockPersonRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(people);

        // Act
        var result = await _mockPersonRepository.Object.GetAllAsync();

        // Assert
        Assert.Equal(people, result);
    }

    [Fact]
    public async Task GetPersonById_Should_ReturnPerson()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" };

        _mockPersonRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(person);

        // Act
        var result = await _mockPersonRepository.Object.GetByIdAsync(1);

        // Assert
        Assert.Equal(person, result);
    }
}
