using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace TestApi.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get() {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}


[ApiController]
[Route("[controller]")]
public class ChickenXmasController : ControllerBase {
    private static byte[]? _chickenPicture;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ChickenXmasController> _logger;

    public ChickenXmasController(ILogger<ChickenXmasController> logger) {
        _logger = logger;
        if (_chickenPicture != null)
            return; 
        
        try {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"TestApi.Assets.chickenDreams.jpg");
            if (stream == null)
                throw new Exception("No chicken image");

            using (Stream reader = stream) {
                _chickenPicture = reader.ReadAllBytes();
            }

        } catch {
            _chickenPicture = null;
        }
   
    }

    [HttpGet]
    public IActionResult? Get() {
        if (_chickenPicture == null)
            return null;
        return File(_chickenPicture, "image/jpeg");
    }


}
public static class StreamExtensions {
    public static byte[] ReadAllBytes(this Stream instream) {
        if (instream is MemoryStream)
            return ((MemoryStream)instream).ToArray();

        using (var memoryStream = new MemoryStream()) {
            instream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}