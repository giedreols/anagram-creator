using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnagramSolver.WebApp.Controllers
{
    [Route("api/[Controller]")]
    public class AnagramicaController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AnagramicaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // kur deti kontroleri? cia ar business logikoj? ar isvis ji taip daryti ar kazkaip kitaip?
        // siaip nelabai suprantu uzduoti, nes mano funkcionalumas skiriasi nuo tu controlleriu, kurie yra anagramicoj
        // aa turbut uzduotis sako, kad anagramas generuoti turi sita anagramica, o ne mano Business logica. bet pas mane ne business logika ir generuoja...
        [HttpGet("all/{letters}")]
        public async Task<IActionResult> GetAllWordsAsync(string letters = "")
        {
            string apiUrl = $"http://www.anagramica.com/all/{letters}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                AnagramApiResponse apiResponse = JsonConvert.DeserializeObject<AnagramApiResponse>(responseBody);

                // pasikonvertuot i kazka, ka priima mano frontas

                return Ok(apiResponse);
            }
            else
            {
                return BadRequest("An error occurred while fetching data from the API.");
            }
        }

    }
}
