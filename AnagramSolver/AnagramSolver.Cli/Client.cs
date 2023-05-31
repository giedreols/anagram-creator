using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli
{
	internal class Client
	{
		private HttpClient httpClient;

		public Client()
		{
			httpClient = new HttpClient();
		}

		internal async Task<AnagramWordsModel?> ExecuteGetAnagramsAsync(string word)
		{
			string url = "http://localhost:5254/api/anagrams/" + word;
			HttpResponseMessage response = await httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				var result = new Converter<AnagramWordsModel>().ConvertFromJson(content);

				return result;
			}
			else
			{
				return null;
			}
		}
	}
}


