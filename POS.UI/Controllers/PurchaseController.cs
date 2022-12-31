using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using POS.DAL.Repository;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;
using System.Text;
using System.Drawing;


namespace POS.UI.Controllers
{
    public class PurchaseController : Controller
    {

        private readonly IWebHostEnvironment _webHost;
        private IUnitOfWork _unitOfWork;
        public PurchaseController(IWebHostEnvironment webHost, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
        }

        public IActionResult Create()
        {
            List<Supplier> supplierList = new List<Supplier>();
            List<Product> productList = new List<Product>();


            supplierList = _unitOfWork.Supplier.GetAll().ToList();
            productList = _unitOfWork.Product.GetAll().ToList();

            PurchaseVM purchaseVM = new()
            {
                Purchase = new(),
                SupplierList = supplierList.Select(i => new SelectListItem
                {
                    Text = i.SupplierName,
                    Value = i.Id.ToString()
                }),
                ProductList = productList.Select(i => new SelectListItem
                {
                    Text = i.Name + " - " + i.Code,
                    Value = i.Id.ToString()
                }),
            };

            return View(purchaseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PurchaseVM obj)
        {

            if (ModelState.IsValid)
            {

            }
             
            return View();
        }
    }
}
