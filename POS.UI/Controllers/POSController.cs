using Microsoft.AspNetCore.Mvc;

namespace POS.UI.Controllers
{
    public class POSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
