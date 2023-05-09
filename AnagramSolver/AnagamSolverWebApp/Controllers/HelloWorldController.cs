using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace AnagamSolverWebApp.Controllers
{

    // Every public method in a controller is callable as an HTTP endpoint.
    // /[Controller]/[ActionName]/[Parameters] The routing format is set in the Program.cs file.

    public class HelloWorldController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        // 
        // GET: /HelloWorld/
        public string Index()
        {
            return "This is my default action...";
        }
        // 
        // GET: /HelloWorld/Welcome/
        //   The name and numTimes parameters are passed in the query string.
        // http://localhost:5254/HelloWorld/Welcome?name=Giedre&numtimes=5
        // arba prie parametrų rašyti ID, ir ta parametrą paduoti kaip /[parameters]
        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }
    }
}
