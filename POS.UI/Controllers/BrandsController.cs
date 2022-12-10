using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;

using System.Text;

namespace POS.UI.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;

        public BrandsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this._unitOfWork = unitOfWork;
            _webHost = webHost;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Upsert(int? id)
        {

           BrandVM brandVM=new()
           {
               Brand=new()
           };
            if (id==null || id==0)
            {
                return View(brandVM);
            }
            else
            {
               brandVM.Brand= _unitOfWork.Brand.GetFirstOrDefault(x => x.Id == id);
                return View(brandVM);
            }



            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(BrandVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var model = new Brand();
                model = obj.Brand;
                string mmRoot = _webHost.WebRootPath;


                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string filePath = Path.Combine(mmRoot, @"img\BrandImages");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var fileExtention = file.FileName;
                    if (obj.Brand.LogoUrl != null)
                    {
                        string oldPath = Path.Combine(mmRoot, obj.Brand.LogoUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName + fileExtention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.LogoUrl = @"\img\BrandImages\" + fileName + fileExtention;

                }
                Brand brand = obj.Brand;
                //Create Product
                if (obj.Brand.Id == 0)
                {
                   
                    _unitOfWork.Brand.Add(brand);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

                //Update Products
                else
                {
                    _unitOfWork.Brand.Update(brand);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

            }
            return View(obj);
        }

      

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var brandList = _unitOfWork.Brand.GetAll();

            return Json(new { data = brandList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var brand = _unitOfWork.Brand.GetFirstOrDefault(f => f.Id == id);
            if (brand == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Brand.Remove(brand);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion
    }
}
