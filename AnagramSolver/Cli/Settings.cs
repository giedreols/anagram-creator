

using System.Runtime;

namespace BusinessLogic
{
    public static class Settings
    {
        public static int TotalAmount = 10;
        public static int MinLength = 4;

        //public IConfigurationRoot Configuration { get; set; }
        public static void ReadSettings()
        {
            //    var builder = WebApplication.CreateBuilder(args);
            //    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //    var appSettings = builder.Configuration.GetSection("MySettings").Get<MySettings>();
            //    builder.Services.AddSingleton(appSettings);
        }

    }
}