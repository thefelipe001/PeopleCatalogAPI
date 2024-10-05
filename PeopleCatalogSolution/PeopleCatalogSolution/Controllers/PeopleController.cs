using Microsoft.AspNetCore.Mvc;
using PeopleCatalog.Application.Interfaces;
using PeopleCatalog.Application.DTOs;

namespace PeopleCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;

        // Constructor donde se inyecta el servicio IPersonService
        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        // Obtener todas las personas
        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await _personService.GetAllPeople();
            return Ok(people);
        }

        // Obtener una persona por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // Crear una nueva persona
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personId = await _personService.CreatePerson(personDto);
            return CreatedAtAction(nameof(GetPersonById), new { id = personId }, personDto);
        }

        // Actualizar una persona
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _personService.UpdatePerson(id, personDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Eliminar una persona
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _personService.DeletePerson(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

