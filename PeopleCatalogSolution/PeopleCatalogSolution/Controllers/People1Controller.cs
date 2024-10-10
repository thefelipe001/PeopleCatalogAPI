using Microsoft.AspNetCore.Mvc;
using PeopleCatalog.Application.Commands;
using PeopleCatalog.Application.Queries;
using PeopleCatalog.Application.DTOs;
using MediatR;
using PeopleCatalog.Domain.Exceptions;

namespace PeopleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class People1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public People1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Obtener todas las personas
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            try
            {
                var people = await _mediator.Send(new GetAllPeopleQuery());
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }
        
        
        }

        // Obtener una persona por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var person = await _mediator.Send(new GetPersonByIdQuery(id));

                if (person == null)
                {
                    return NotFound(new { message = $"Persona con ID {id} no encontrada." });
                }
                return Ok(person);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }
        }


        // Crear una nueva persona
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createPersonCommand = new CreatePersonCommand
                {
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    Email = personDto.Email,
                    DateOfBirth = personDto.DateOfBirth,
                    PhoneNumber = personDto.PhoneNumber,
                    Street = personDto.Street,
                    City = personDto.City,
                    PostalCode = personDto.PostalCode
                };

                var personId = await _mediator.Send(createPersonCommand);
                return CreatedAtAction(nameof(GetPersonById), new { id = personId }, personDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }
        }


        // Actualizar una persona existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDto personDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatePersonCommand = new UpdatePersonCommand
                {
                    Id = id,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    Email = personDto.Email,
                    DateOfBirth = personDto.DateOfBirth,
                    PhoneNumber = personDto.PhoneNumber,
                    Street = personDto.Street,
                    City = personDto.City,
                    PostalCode = personDto.PostalCode
                };

                var result = await _mediator.Send(updatePersonCommand);

                if (!result)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }


        }



        // Eliminar una persona por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var deletePersonCommand = new DeletePersonCommand(id);
                var result = await _mediator.Send(deletePersonCommand);
                if (!result)
                {
                    return NotFound(new { message = $"Persona con ID {id} no encontrada para eliminación." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }
          
        }


        //Consultar las Personas mayores de Edad
        [HttpGet("age/{age}")]
        public async Task<IActionResult> GetPersonByAge(int age)
        {
            try
            {
                var people = await _mediator.Send(new GetPersonByAgeQuery(age));

                // Verifica si la lista está vacía
                if (people == null || !people.Any())
                {
                    return NotFound(new { message = $"No se encontró ninguna persona con la edad de {age} o mayor." });
                }

                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }
        }


        // Obtener todas las personas
        [HttpGet("GetAllPeopleforView")]
        public async Task<IActionResult> GetAllPeopleforView()
        {
            try
            {
                var people = await _mediator.Send(new GetAllPeopleViewQuery());
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }


        }



    }
}
