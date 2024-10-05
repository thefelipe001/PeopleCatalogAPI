using Microsoft.AspNetCore.Mvc;
using Moq;
using PeopleCatalog.API.Controllers;
using PeopleCatalog.Application.DTOs;
using PeopleCatalog.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ApiTests
{
    private readonly Mock<IPersonService> _mockPersonService;
    private readonly PeopleController _controller;

    public ApiTests()
    {
        _mockPersonService = new Mock<IPersonService>();
        _controller = new PeopleController(_mockPersonService.Object);
    }

    [Fact]
    public async Task GetAllPeople_Should_ReturnOkResult()
    {
        // Arrange
        var people = new List<PersonDto>
        {
            new PersonDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
            new PersonDto { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com" }
        };

        _mockPersonService.Setup(service => service.GetAllPeople())
            .ReturnsAsync(people);

        // Act
        var result = await _controller.GetAllPeople();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnPeople = Assert.IsType<List<PersonDto>>(okResult.Value);
        Assert.Equal(people.Count, returnPeople.Count);
    }

    [Fact]
    public async Task GetPersonById_Should_ReturnNotFound()
    {
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
        var result = await _controller.GetPersonById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreatePerson_Should_ReturnCreatedAtAction()
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

        _mockPersonService.Setup(service => service.CreatePerson(It.IsAny<PersonDto>()))
            .ReturnsAsync(1);

        // Act
        var result = await _controller.CreatePerson(personDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
    }
}
