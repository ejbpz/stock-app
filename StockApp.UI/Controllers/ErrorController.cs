using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace StockApp.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [HttpGet("")]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(error?.Error.Message);
        }
    }
}
