using ByteBank.Portal.Filters;
using ByteBank.Service;
using ByteBank.Service.Cambio;

namespace ByteBank.Portal.Controller;

public class ErrorController : ControllerBase
{
    public string Unexpected() => View();
}