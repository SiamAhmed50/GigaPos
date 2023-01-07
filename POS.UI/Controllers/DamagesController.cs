using Microsoft.AspNetCore.Mvc;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace POS.UI.Controllers
{
    public class DamagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public DamagesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            ViewBag.ProductList = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            List<Product> productList = new List<Product>();
            productList = _unitOfWork.Product.GetAll().ToList();
            foreach (Product product in productList)
            {
                product.Unit = _unitOfWork.Unit.GetFirstOrDefault(f => f.Id == product.UnitId);
            }
            DamageVM damageVM = new()
            {
                Damage = new(),
                ProductList = productList.Select(i => new SelectListItem
                {
                    Text = i.Name + " - " + "Stock: " + i.Stock.ToString() + i.Unit.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null || id == 0)
            {
                ViewBag.ProductList = new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
                return View(damageVM);
            }
            else
            {
                damageVM.Damage = _unitOfWork.Damage.GetFirstOrDefault(x => x.Id == id);
                return View(damageVM);
            }



            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(DamageVM obj)
        {
            if (ModelState.IsValid)
            {
                var model = new Damage();
                model = obj.Damage;
                //Create Product
                if (obj.Damage.Id == 0)
                {

                    _unitOfWork.Damage.Add(model);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

                //Update Products
                else
                {
                    _unitOfWork.Damage.Update(model);
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
            var damageList = _unitOfWork.Damage.GetAll();

            return Json(new { data = damageList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var damage = _unitOfWork.Damage.GetFirstOrDefault(f => f.Id == id);
            if (damage == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.Damage.Remove(damage);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });
        }
        #endregion
    }
}
