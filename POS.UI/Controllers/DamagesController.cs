using Microsoft.AspNetCore.Mvc;

namespace POS.UI.Controllers
{
    public class DamagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert()
        {
            return View();
        }
    }
}
