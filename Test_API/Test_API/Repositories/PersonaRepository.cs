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

        public async Task<IEnumerable<Persona>> SearchPersonasAsync(string search)
        {
            return await _context.Personas
                .Where(p =>
                    p.Identificacion.Contains(search) ||
                    p.Nombre.Contains(search) ||
                    p.ApellidoPaterno.Contains(search) ||
                    p.ApellidoMaterno.Contains(search) 
                )
                .ToListAsync();
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

        public async Task<bool> DeletePersonaByIdentificacion(string identificacion)
        {
            var persona = await DeleteByIdentificacion(identificacion);
            if (persona == null) return false;
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> StorePersona(Persona persona)
        {
            bool existe = await _context.Personas
                .AnyAsync(p => p.Identificacion == persona.Identificacion);
            if (existe) return false; 

            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
