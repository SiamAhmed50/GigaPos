using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;

namespace POS.UI.Controllers
{
    public class POSController : Controller
    {

        private readonly IWebHostEnvironment _webHost;
        private IUnitOfWork _unitOfWork;
        public POSController(IWebHostEnvironment webHost, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
           
            List<Product> productList = new List<Product>();
            List<Customer> customerList = new List<Customer>();
            List<Category> categoryList = new List<Category>();

            customerList = _unitOfWork.Customer.GetAll().ToList();
            productList = _unitOfWork.Product.GetAll().ToList();
            categoryList = _unitOfWork.Category.GetAll().ToList();

            PosVM purchaseVM = new()
            {
                Pos = new(), 
                ProductList = productList.Select(i => new SelectListItem
                {
                    Text = i.Name + " - " + i.Code,
                    Value = i.Id.ToString()
                }),

                CustomerList = customerList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CategoryList = categoryList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            return View(purchaseVM);
        }


        #region API Calls
        [HttpGet]
        public IActionResult GetProducts(string? searchFilter,int? categoryId)
        {
            var productList = _unitOfWork.Product.GetAll();
            if(searchFilter != "null" && searchFilter != "undefined")
            {
                productList = productList.Where(w => w.Name.ToLower().Contains(searchFilter.ToLower()) || (!string.IsNullOrWhiteSpace(w.Code)) &&  w.Code.ToLower().Contains(searchFilter.ToLower())).ToList();
            }
            if(categoryId != null)
            {
                productList = productList.Where(w => w.CategoryId == categoryId).ToList();
            }


            return Json(new { data = productList });
        } 
        #endregion
    }
}
