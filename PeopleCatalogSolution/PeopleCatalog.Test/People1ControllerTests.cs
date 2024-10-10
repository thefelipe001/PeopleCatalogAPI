using Microsoft.AspNetCore.Mvc;
using PeopleCatalog.Application.DTOs;
using PeopleCatalog.API.Controllers;
using Moq;
using MediatR;
using PeopleCatalog.Application.Queries;
using PeopleCatalog.Application.Commands;

namespace PeopleCatalog.Test
{
    [TestFixture]
    public class People1ControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private People1Controller _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new People1Controller(_mockMediator.Object);
        }

        [Test]
        public async Task GetAllPeople_Should_ReturnOkResult()
        {
            // Arrange
            var people = new List<PersonDto>
            {
                new PersonDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
                new PersonDto { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com" }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllPeopleQuery>(), default))
                         .ReturnsAsync(people);

            // Act
            var result = await _controller.GetAllPeople();

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var returnPeople = okResult?.Value as List<PersonDto>;
            Assert.That(returnPeople, Is.Not.Null);
            Assert.That(returnPeople.Count, Is.EqualTo(people.Count));
        }

        [Test]
        public async Task GetPersonById_Should_ReturnNotFound_When_PersonNotExists()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), default))
                         .ReturnsAsync((PersonDto)null);

            // Act
            var result = await _controller.GetPersonById(1);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetPersonById_Should_ReturnOkResult_When_PersonExists()
        {
            // Arrange
            var person = new PersonDto { Id = 1, FirstName = "John", LastName = "Doe" };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), default))
                         .ReturnsAsync(person);

            // Act
            var result = await _controller.GetPersonById(1);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            var returnPerson = okResult?.Value as PersonDto;

            Assert.That(returnPerson, Is.Not.Null);
            Assert.That(returnPerson.Id, Is.EqualTo(1));
        }

        [Test]
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

            _mockMediator.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), default))
                         .ReturnsAsync(1);

            // Act
            var result = await _controller.CreatePerson(personDto);

            // Assert
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());

            var createdResult = result as CreatedAtActionResult;
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.ActionName, Is.EqualTo(nameof(_controller.GetPersonById)));
            Assert.That(createdResult.RouteValues["id"], Is.EqualTo(1));
        }

        [Test]
        public async Task UpdatePerson_Should_ReturnNotFound_When_PersonNotExists()
        {
            // Arrange
            var personDto = new PersonDto
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), default))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdatePerson(1, personDto);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdatePerson_Should_ReturnOkResult_When_PersonExists()
        {
            // Arrange
            var personDto = new PersonDto
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), default))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdatePerson(1, personDto);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetPersonByAge_Should_ReturnOkResult_WithPeople()
        {
            // Arrange
            var people = new List<PersonDto>
            {
                new PersonDto { Id = 1, FirstName = "John", LastName = "Doe" }
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetPersonByAgeQuery>(), default))
                         .ReturnsAsync(people);

            // Act
            var result = await _controller.GetPersonByAge(30);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            var returnPeople = okResult?.Value as List<PersonDto>;

            Assert.That(returnPeople, Is.Not.Null);
            Assert.That(returnPeople.Count, Is.EqualTo(people.Count));
        }

        [Test]
        public async Task GetPersonByAge_Should_ReturnNotFound_When_NoPeopleFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetPersonByAgeQuery>(), default))
                         .ReturnsAsync(new List<PersonDto>());

            // Act
            var result = await _controller.GetPersonByAge(30);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}