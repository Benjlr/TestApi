using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChickenXmasController : ControllerBase {
    private static byte[]? _chickenPicture;

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
