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

        public async Task<IEnumerable<Factura>> GetAllFacturas()
        {
            return await _facturaRepository.FindFacturas();
        }

        public async Task<IEnumerable<Factura>> GetFacturasByPersona(int personaId)
        {
            return await _facturaRepository.FindFacturasByPersona(personaId);
        }

        public async Task<bool> SaveFactura(Factura factura)
        {
            return await _facturaRepository.StoreFactura(factura);
        }
    }
}
