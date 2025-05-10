namespace WpfTest.Models.Dtos
{
    class FacturaDto
    {
        public DateOnly? Fecha { get; set; }

        public decimal? Monto { get; set; }

        public int? PersonaId { get; set; }
    }
}
