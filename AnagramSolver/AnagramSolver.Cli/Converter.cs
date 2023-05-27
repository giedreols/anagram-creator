using Newtonsoft.Json;

namespace AnagramSolver.Cli
{
	public class Converter<T>
	{
		public T ConvertFromJson(string text)
		{
			return JsonConvert.DeserializeObject<T>(text);
		}
	}
}
