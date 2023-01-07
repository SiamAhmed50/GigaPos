using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using POS.DAL.Repository;
using POS.DAL.Repository.IRpository;
using POS.Models.AppVM;
using POS.Models.EntityModel;
using System.Text;
using System.Drawing;
using POS.Models.Enums;

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

        public IActionResult Invoice(int Id)
        {
            var purchase = _unitOfWork.Purchase.GetFirstOrDefault(f => f.Id == Id);
            purchase.PurchaseItems = _unitOfWork.Purchase.GetPurchaseItems(Id);
            foreach(var item in purchase.PurchaseItems)
            {
                item.Product = _unitOfWork.Product.GetFirstOrDefault(f => f.Id == item.ProductId);
            }
            var supplier = _unitOfWork.Supplier.GetFirstOrDefault(f => f.Id == purchase.SupplierId);
            var PurchaseInvoice = new PurchaseInvoiceVM();
            PurchaseInvoice.InvoiceId = purchase.Id.ToString();
            PurchaseInvoice.Date = DateTime.Now.ToString("dd/mm/yyyy");
            PurchaseInvoice.Note = purchase.Note;
            PurchaseInvoice.Due = purchase.Due.ToString();
            PurchaseInvoice.Paid = purchase.Paid.ToString();
            PurchaseInvoice.GrandTotal = purchase.GrandTotal.ToString();
            PurchaseInvoice.SupplierName = supplier.SupplierName.ToString();
            PurchaseInvoice.Address = supplier.Address != null ? supplier.Address: "";
            PurchaseInvoice.Phone = supplier.Phone.ToString();
            PurchaseInvoice.Email = supplier.Email != null ? supplier.Email:"";
            PurchaseInvoice.TransactionAccount = TransactionAccount.CASH.ToString();
            List<PurchaseItemsInvoiceVM> purchaseItems = new List<PurchaseItemsInvoiceVM>();
            foreach(var product in purchase.PurchaseItems)
            {
                var obj = new PurchaseItemsInvoiceVM();
                obj.ProductName = product.Product != null ? product.Product.Name:"";
                obj.ProductCode = product.Product != null ? product.Product.Code : "";
                obj.Price = product.Product != null ? product.Product.SalePrice.ToString() : ""; 
                obj.SubTotal = product.SubTotal.ToString(); 

                obj.UnitName = _unitOfWork.Unit.GetFirstOrDefault(f => f.Id == product.Product.UnitId).Name;
                obj.MainUnitQuantity = product.Product != null ? product.Product.OpenningStockSubUnit.ToString(): "";
                if(product.Product.SubUnitId > 0)
                {
                    obj.SubUnitName = _unitOfWork.Unit.GetFirstOrDefault(f => f.Id == product.Product.SubUnitId).Name;
                    obj.SubUnitQuantity = product.Product != null ? product.Product.OpenningStockSubUnit.ToString() : "";
                }


                purchaseItems.Add(obj);
            }
            PurchaseInvoice.PurchaseItems= purchaseItems;

            return View(PurchaseInvoice);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PurchaseVM obj)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    var purchase = new Purchase();
                    purchase = obj.Purchase;
                    _unitOfWork.Purchase.Add(purchase);
                    //Update Product Stock
                    foreach(var product in purchase.PurchaseItems)
                    {
                        var Product = _unitOfWork.Product.GetFirstOrDefault(f => f.Id == product.ProductId);
                        Product.Stock += product.Quantity;

                    }
                    _unitOfWork.Save();
                    return RedirectToAction("Invoice", new { id = purchase.Id });
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }

            return View();
        }
      
      

    }
}
