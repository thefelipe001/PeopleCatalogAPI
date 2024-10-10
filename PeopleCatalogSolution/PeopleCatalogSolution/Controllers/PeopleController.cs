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

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            try
            {
                var people = await _personService.GetAllPeople();
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de personas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            try
            {
                var person = await _personService.GetPersonById(id);
                if (person == null)
                {
                    return NotFound(new { message = $"Persona con ID {id} no encontrada." });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la persona: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Los datos enviados no son válidos.", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                var personId = await _personService.CreatePerson(personDto);
                return CreatedAtAction(nameof(GetPersonById), new { id = personId }, personDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la persona: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDto personDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Los datos enviados no son válidos.", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                var result = await _personService.UpdatePerson(id, personDto);
                if (!result)
                {
                    return NotFound(new { message = $"Persona con ID {id} no encontrada para actualización." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la persona: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _personService.DeletePerson(id);
                if (!result)
                {
                    return NotFound(new { message = $"Persona con ID {id} no encontrada para eliminación." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la persona: {ex.Message}");
            }
        }



    }
}
