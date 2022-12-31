using Microsoft.AspNetCore.Mvc;

namespace POS.UI.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
