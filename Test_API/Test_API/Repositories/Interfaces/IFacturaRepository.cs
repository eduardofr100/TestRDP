using Test_API.Models.Entities;

namespace Test_API.Repositories.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> FindFacturasByPersona(int personaId);
        Task StoreFactura(Factura factura);
    }
}
