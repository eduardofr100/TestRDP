using Test_API.Models.Entities;
using Test_API.Repositories.Interfaces;

namespace Test_API.Services
{
    public class PersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<Persona> GetPersonaByIdentificacion(string identificacion)
        {
            return await _personaRepository.FindPersonaByIdentificacion(identificacion);
        }

        public async Task<IEnumerable<Persona>> GetAllPersonas()
        {
            return await _personaRepository.FindPersonas();
        }

        public async Task DeletePersona(string identificacion)
        {
            await _personaRepository.DeletePersonaByIdentificacion(identificacion);
        }

        public async Task SavePersona(Persona persona)
        {
            await _personaRepository.StorePersona(persona);
        }
    }
}
