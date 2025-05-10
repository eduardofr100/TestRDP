using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

        public async Task<IEnumerable<Factura>> FindFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        public async Task<IEnumerable<Factura>> FindFacturasByPersona(int personaId)
        {
            return await _context.Facturas
                .Where(f => f.PersonaId == personaId)
                .ToListAsync();
        }

        public async Task<bool> StoreFactura(Factura factura)
        {
            bool existe = await _context.Facturas
                .AnyAsync(p => p.IdFactura == factura.IdFactura);
            if (existe) return false;
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
