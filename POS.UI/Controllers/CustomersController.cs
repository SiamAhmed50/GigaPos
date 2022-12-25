using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;

namespace POS.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        public CustomersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
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
            List<Customer> unitList = new List<Customer>();
            unitList = _unitOfWork.Customer.GetAll().ToList();

            CustomerVM customerVM = new()
            {
                Customer = new(),
                
            };
            if (id == null || id == 0)
            {

                return View(customerVM);
            }
            else
            {
                customerVM.Customer = _unitOfWork.Customer.GetFirstOrDefault(x => x.Id == id);
                return View(customerVM);
            }



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CustomerVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Customer.Id == 0)
                    {
                        _unitOfWork.Customer.Add(model.Customer);
                    }
                    else
                    {
                        _unitOfWork.Customer.Update(model.Customer);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Customer Created Succesfully!";
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
            var customerList = _unitOfWork.Customer.GetAll();

            return Json(new { data = customerList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var customer = _unitOfWork.Customer.GetFirstOrDefault(f => f.Id == id);
            if (customer == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Customer.Remove(customer);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion
    }
}
