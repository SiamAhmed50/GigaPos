using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<Unit> unitList = new List<Unit>();
            unitList = _unitOfWork.Unit.GetAll().ToList();

            UnitVM unitVM = new()
            {
                Unit = new(),
                UnitList = unitList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
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
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UnitVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Unit.Id == 0)
                    {
                        _unitOfWork.Unit.Add(model.Unit);
                    }
                    else
                    {
                        _unitOfWork.Unit.Update(model.Unit);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Unit Created Succesfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                }
            }
            return View(model);
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
