using System.Diagnostics;
using Assignment_Sa.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_Sa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/Error")]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            ViewBag.ErrorMessage = exceptionFeature?.Error.Message;

            return View();
        }
    }
}
