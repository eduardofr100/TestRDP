using Microsoft.EntityFrameworkCore;
using Test_API.Migrations;
using Test_API.Models.Entities;
using Test_API.Repositories.Interfaces;

namespace Test_API.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly TestRdpContext _context;

        public PersonaRepository(TestRdpContext context)
        {
            _context = context;
        }

        public async Task<Persona> FindPersonaByIdentificacion(string identificacion)
        {
            return await _context.Personas
                .FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }

        public async Task<IEnumerable<Persona>> FindPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona> DeleteByIdentificacion(string identificacion)
        {
            return await _context.Personas
                .Include(p => p.Facturas)
                .FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }

        public async Task DeletePersonaByIdentificacion(string identificacion)
        {
            var persona = await DeleteByIdentificacion(identificacion);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public async Task StorePersona(Persona persona)
        {
            if (persona.IdPersona == 0)
            {
                await _context.Personas.AddAsync(persona);
            }
            //else
            //{
            //    _context.Personas.Update(persona);
            //}
            await _context.SaveChangesAsync();
        }
    }
}
