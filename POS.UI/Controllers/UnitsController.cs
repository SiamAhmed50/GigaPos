using Microsoft.AspNetCore.Mvc;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;

namespace POS.UI.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
       

        public IActionResult Upsert(int? id)
        {

            UnitVM unitVM = new()
            {
                Unit = new()
            };
            if (id == null || id == 0)
            {
                return View(unitVM);
            }
            else
            {
                unitVM.Unit = _unitOfWork.Unit.GetFirstOrDefault(x => x.Id == id);
                return View(unitVM);
            }



            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(UnitVM obj)
        {
            if (ModelState.IsValid)
            {
                var model = new Unit();
                model = obj.Unit;
                
                //Create Product
                if (model.Id == 0)
                {

                    _unitOfWork.Unit.Add(model);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

                //Update Products
                else
                {
                    _unitOfWork.Unit.Update(model);
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
            var unitList = _unitOfWork.Unit.GetAll();

            return Json(new { data = unitList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var unit = _unitOfWork.Unit.GetFirstOrDefault(f => f.Id == id);
            if (unit == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Unit.Remove(unit);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion
    }
}
