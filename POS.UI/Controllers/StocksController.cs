
using POS.DAL.Repository.IRpository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace POS.UI.Controllers
{
    public class StocksController :Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private IUnitOfWork _unitOfWork;
        public StocksController(IWebHostEnvironment webHost, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
        }
        public IActionResult Index()
        {


            ViewBag.ProductList = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
            ViewBag.CategoryList = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            ViewBag.BrandList = new SelectList(_unitOfWork.Brand.GetAll(), "Id", "Name");
            return View();
        }
    }
}
