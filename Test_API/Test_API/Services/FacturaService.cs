using Test_API.Models.Entities;
using Test_API.Repositories.Interfaces;

namespace Test_API.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IPersonaRepository _personaRepository;

        public FacturaService(IFacturaRepository facturaRepository, IPersonaRepository personaRepository)
        {
            _facturaRepository = facturaRepository;
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<Factura>> GetFacturasByPersona(int personaId)
        {
            return await _facturaRepository.FindFacturasByPersona(personaId);
        }

        public async Task SaveFactura(Factura factura)
        {
            // Validar que la persona existe
            var persona = await _personaRepository.FindPersonaByIdentificacion(factura.Persona.Identificacion);
            if (persona == null)
            {
                throw new ArgumentException("La persona no existe");
            }

            factura.PersonaId = persona.IdPersona;
            await _facturaRepository.StoreFactura(factura);
        }
    }
}
