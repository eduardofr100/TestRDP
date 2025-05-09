using Test_API.Models.Entities;

namespace Test_API.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> FindPersonaByIdentificacion(string identificacion);
        Task<IEnumerable<Persona>> FindPersonas();
        Task DeletePersonaByIdentificacion(string identificacion);
        Task StorePersona(Persona persona);
    }
}
