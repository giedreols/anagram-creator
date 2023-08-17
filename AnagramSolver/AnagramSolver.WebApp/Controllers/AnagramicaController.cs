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

        [HttpGet("all/{letters}")]
        public async Task<IActionResult> GetAllWordsAsync(string letters = "")
        {
            string apiUrl = $"http://www.anagramica.com/all/{letters}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                AnagramApiResponse apiResponse = JsonConvert.DeserializeObject<AnagramApiResponse>(responseBody);
                return Ok(apiResponse);
            }
            else
            {
                return BadRequest("An error occurred while fetching data from the API.");
            }
        }

    }
}
