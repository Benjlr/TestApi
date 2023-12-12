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


[ApiController]
[Route("[controller]")]
public class BirthdayController : ControllerBase {
    private static int _birthdayCount = 0;
    private readonly ILogger<BirthdayController> _logger;

    public BirthdayController(ILogger<BirthdayController> logger) {
        _logger = logger; 
    }

    [HttpGet]
    public ContentResult Get() { 
        _birthdayCount++;

        return Content($"<h1>HAPPY BIRTHDAY DAD!</h1><br /><br /><h3>{_birthdayCount} peeps have said happy birthday</h3>", "text/html");
    }


}
