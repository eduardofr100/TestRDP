namespace Test_API.Models.Entities;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly? Fecha { get; set; }

    public decimal? Monto { get; set; }

    public int? PersonaId { get; set; }

    public virtual Persona? Persona { get; set; }
}
