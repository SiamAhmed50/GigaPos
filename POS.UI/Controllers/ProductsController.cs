using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using POS.DAL.Repository;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;

using System.Text;

namespace POS.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        private IUnitOfWork _unitOfWork;
        public ProductsController(IWebHostEnvironment webHost, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Upsert(int? id)
        {
            

            // category item for dropdown
            List<Category> categoryList = new List<Category>();
           

           
            List<Unit> unitList = new List<Unit>();
            List<Brand> brandList = new List<Brand>();
           

            categoryList = _unitOfWork.Category.GetAll().ToList();
            unitList = _unitOfWork.Unit.GetAll().ToList();
            brandList = _unitOfWork.Brand.GetAll().ToList(); 

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = categoryList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                UnitList = unitList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                BrandList = brandList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                return View(productVM);
            }

            
            else
            {
                //update 
                if (id == null)
                {
                    return NotFound();
                }

                    productVM.Product = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
                    return View(productVM);
                
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                var model = new Product();
                model = obj.Product;
                string mmRoot = _webHost.WebRootPath;


                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string filePath = Path.Combine(mmRoot, @"img\ProductImages");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var fileExtention = file.FileName;
                    if (obj.Product.ImageUrl != null)
                    {
                        string oldPath = Path.Combine(mmRoot, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName + fileExtention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ImageUrl = @"\img\ProductImages\" + fileName + fileExtention;

                }
                //Create Product
                if (obj.Product.Id == 0)
                {
                    

                  
                }

                //Update Products
                else
                {
                    _unitOfWork.Product.Update(model);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

            }
            return View(obj);
        }

      


        //API CALLS
        #region API Calles

        [HttpGet]
        public IActionResult GetAll()
        {

                var itemVm = _unitOfWork.Product.GetAll();
                return Json(new { data = itemVm });
            
        }
       
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var product = _unitOfWork.Product.GetFirstOrDefault(f => f.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion
    }
}
