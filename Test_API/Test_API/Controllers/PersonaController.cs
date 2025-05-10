using Microsoft.AspNetCore.Mvc;
using Test_API.Models.Dtos;
using Test_API.Models.Entities;
using Test_API.Services;

namespace Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly PersonaService _personaService;

        public PersonasController(PersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet("{identificacion}")]
        public async Task<ActionResult<Persona>> GetByIdentificacion(string identificacion)
        {
            var persona = await _personaService.GetPersonaByIdentificacion(identificacion);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Persona>>> Search([FromQuery] string search)
        {
            var personas = await _personaService.SearchPersonasAsync(search);
            if (!personas.Any())
            {
                return NotFound();
            }
            return Ok(personas);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            return Ok(await _personaService.GetAllPersonas());
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> Create(PersonaDto personaDto)
        {
            var persona = new Persona
            {
                Nombre = personaDto.Nombre,
                ApellidoPaterno = personaDto.ApellidoPaterno,
                ApellidoMaterno = personaDto.ApellidoMaterno,
                Identificacion = personaDto.Identificacion
            };
            bool success = await _personaService.SavePersona(persona);
            if (!success)
            {
                return BadRequest("No se pudo guardar la persona.");
            }
            return CreatedAtAction(nameof(GetByIdentificacion), new { identificacion = persona.Identificacion }, persona);
        }

        [HttpDelete("{identificacion}")]
        public async Task<IActionResult> Delete(string identificacion)
        {
            bool success = await _personaService.DeletePersona(identificacion);
            return success ? NoContent() : NotFound();
        }
    }
}
