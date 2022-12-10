using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace POS.UI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
