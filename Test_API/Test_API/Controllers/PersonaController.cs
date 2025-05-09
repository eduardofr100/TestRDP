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
            await _personaService.SavePersona(persona);
            return CreatedAtAction(nameof(GetByIdentificacion), new { identificacion = persona.Identificacion }, persona);
        }

        [HttpDelete("{identificacion}")]
        public async Task<IActionResult> Delete(string identificacion)
        {
            await _personaService.DeletePersona(identificacion);
            return NoContent();
        }
    }
}
