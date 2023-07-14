using Newtonsoft.Json;

namespace AnagramSolver.Cli
{
    [Obsolete("new implementation uses WebApp")]
    public class Converter<T>
    {
        public T ConvertFromJson(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
