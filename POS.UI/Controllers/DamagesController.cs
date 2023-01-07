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
            return View();
        }

        public IActionResult Upsert(int? id)
        {

            DamageVM damageVM = new()
            {
                Damage = new()
            };
            if (id == null || id == 0)
            {
                ViewBag.ProductList=new SelectList(_unitOfWork.Product.GetAll(), "Id", "Name");
				return View(damageVM);
            }
            else
            {
                damageVM.Damage = _unitOfWork.Damage.GetFirstOrDefault(x => x.Id == id);
                return View(damageVM);
            }


 
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
    }
}
