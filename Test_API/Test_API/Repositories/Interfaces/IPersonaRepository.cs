using Test_API.Models.Entities;

namespace Test_API.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> FindPersonaByIdentificacion(string identificacion);
        Task<IEnumerable<Persona>> SearchPersonasAsync(string search);
        Task<IEnumerable<Persona>> FindPersonas();
        Task<bool> DeletePersonaByIdentificacion(string identificacion);
        Task<bool> StorePersona(Persona persona);
    }
}
