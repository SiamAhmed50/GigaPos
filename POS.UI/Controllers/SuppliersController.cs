using Microsoft.AspNetCore.Mvc;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;

namespace POS.UI.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        public SuppliersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            this._webHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Upsert(int? id)
        {
            List<Supplier> supplierList = new List<Supplier>();
            supplierList = _unitOfWork.Supplier.GetAll().ToList();

            SupplierVM supplierVM = new()
            {
                Supplier = new(),

            };
            if (id == null || id == 0)
            {

                return View(supplierVM);
            }
            else
            {
                supplierVM.Supplier = _unitOfWork.Supplier.GetFirstOrDefault(x => x.Id == id);
                return View(supplierVM);
            }



         
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(SupplierVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Supplier.Id == 0)
                    {
                        _unitOfWork.Supplier.Add(model.Supplier);
                    }
                    else
                    {
                        _unitOfWork.Supplier.Update(model.Supplier);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Supplier Created Succesfully!";
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
            var supplierList = _unitOfWork.Supplier.GetAll();

            return Json(new { data = supplierList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var supplier = _unitOfWork.Supplier.GetFirstOrDefault(f => f.Id == id);
            if (supplier == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Supplier.Remove(supplier);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion


    }
}
