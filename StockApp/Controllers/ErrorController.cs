using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
