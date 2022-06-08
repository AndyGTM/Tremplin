using Microsoft.AspNetCore.Mvc;

namespace Tremplin.Controllers
{
    public class HealthProfessionalController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}