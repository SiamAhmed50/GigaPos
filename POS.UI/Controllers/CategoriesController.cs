using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POS.DAL.Repository.IRpository;
using POS.Models.ProductModel;
using POS.UI.Models.ViewModels;
using System.Text;

namespace POS.UI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(model);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Succesfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetFirstOrDefault(f => f.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Succesfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
       
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitOfWork.Category.GetAll();

            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var product = _unitOfWork.Category.GetFirstOrDefault(f => f.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            
            _unitOfWork.Category.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion
    }
}
