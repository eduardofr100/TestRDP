using Microsoft.AspNetCore.Mvc;
using Test_API.Models.Dtos;
using Test_API.Models.Entities;
using Test_API.Services;

namespace Test_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _facturaService;

        public FacturasController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetAll()
        {
            return Ok(await _facturaService.GetAllFacturas());
        }

        [HttpGet("{personaId}")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetByFactura(int personaId)
        {
            return Ok(await _facturaService.GetFacturasByPersona(personaId));
        }

        [HttpPost]
        public async Task<ActionResult<Factura>> Create(FacturaDto facturaDto)
        {
            var factura = new Factura
            {
                Fecha = facturaDto.Fecha,
                Monto = facturaDto.Monto,
                PersonaId = facturaDto.PersonaId
            };
            bool success = await _facturaService.SaveFactura(factura);
            if (!success)
            {
                return BadRequest("No se pudo guardar la persona.");
            }
            return CreatedAtAction(nameof(GetByFactura), new { personaId = factura.PersonaId }, factura);
        }
    }
}
