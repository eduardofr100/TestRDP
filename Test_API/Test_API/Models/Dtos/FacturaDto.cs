namespace Test_API.Models.Dtos
{
    public class FacturaDto
    {
        public DateOnly? Fecha { get; set; }

        public decimal? Monto { get; set; }

        public int? PersonaId { get; set; }
    }
}
