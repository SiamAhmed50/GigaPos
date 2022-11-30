using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.DAL.Repository.IRpository;
using POS.Models.ProductModel;
using System.Net;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Get
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.GetAll();
        }
        //Post
        [HttpPost]
        public HttpResponseMessage Creat(Category obj)
        {
            if (obj==null)
            {
                HttpResponseMessage NotFoundresponse=new HttpResponseMessage(HttpStatusCode.NotFound);
                return NotFoundresponse;
            }
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            HttpResponseMessage Suucessresponse = new HttpResponseMessage(HttpStatusCode.Created);
            return Suucessresponse;
        }
    }
}
