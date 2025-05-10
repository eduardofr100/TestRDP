using Test_API.Models.Entities;

namespace Test_API.Repositories.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> FindFacturas();
        Task<IEnumerable<Factura>> FindFacturasByPersona(int identificacion);
        Task<bool> StoreFactura(Factura factura);
    }
}
