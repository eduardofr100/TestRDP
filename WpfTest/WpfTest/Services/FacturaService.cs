using System.Net.Http;
using WpfTest.Models.Dtos;
using WpfTest.Models.Entities;

namespace WpfTest.Services
{
    class FacturaService
    {
        private readonly ApiService _apiService = new ApiService();

        public async Task<List<Factura>> GetFacturasAsync()
        {
            try
            {
                return await _apiService.GetAsync<List<Factura>>("Facturas");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Factura>();
            }
        }

        public async Task<List<Factura>> GetByFacturaAsync(int identificacion)
        {
            return await _apiService.GetAsync<List<Factura>>($"Facturas/{identificacion}");
        }

        public async Task<bool> CreateFacturaAsync(FacturaDto facturaDto)
        {
            var response = await _apiService.PostAsync("Facturas", facturaDto);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(errorMessage);
            }
        }
    }
}
