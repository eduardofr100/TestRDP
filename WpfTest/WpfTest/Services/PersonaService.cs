using System.Net;
using System.Net.Http;
using WpfTest.Models.Dtos;
using WpfTest.Models.Entities;

namespace WpfTest.Services
{
    class PersonaService
    {
        private readonly ApiService _apiService = new ApiService();

        public async Task<List<Persona>> GetPersonasAsync()
        {
            try
            {
                return await _apiService.GetAsync<List<Persona>>("Personas"); 
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Persona>();
            }
        }

        public async Task<IEnumerable<Persona>> SearchPersonasAsync(string search)
        {
            var response = await _apiService.GetAsync("Personas/search", new Dictionary<string, string> { { "search", search } });
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Persona>>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<Persona> GetByIdentificacionAsync(string identificacion)
        {
            return await _apiService.GetAsync<Persona>($"Personas/{identificacion}");
        }

        public async Task<bool> CreatePersonaAsync(PersonaDto personaDto)
        {
            var response = await _apiService.PostAsync("Personas", personaDto);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error: {response.StatusCode} - {errorContent}");
            }
        }

        public async Task<bool> DeletePersonaAsync(string identificacion)
        {
            var response = await _apiService.DeleteAsync($"Personas/{identificacion}");

            if (response.IsSuccessStatusCode) return true;
            if (response.StatusCode == HttpStatusCode.NotFound) return false;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error: {response.StatusCode} - {errorContent}");
        }
    }
}
