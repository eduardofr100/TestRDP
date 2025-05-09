using Microsoft.EntityFrameworkCore;
using Test_API.Migrations;
using Test_API.Models.Entities;
using Test_API.Repositories.Interfaces;

namespace Test_API.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly TestRdpContext _context;

        public FacturaRepository(TestRdpContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> FindFacturasByPersona(int personaId)
        {
            return await _context.Facturas
                .Where(f => f.PersonaId == personaId)
                .ToListAsync();
        }

        public async Task StoreFactura(Factura factura)
        {
            if (factura.IdFactura == 0)
            {
                await _context.Facturas.AddAsync(factura);
            }
            //else
            //{
            //    _context.Facturas.Update(factura);
            //}
            await _context.SaveChangesAsync();
        }
    }
}
