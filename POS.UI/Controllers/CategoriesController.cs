using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POS.UI.Models.ViewModels;
using System.Text;

namespace POS.UI.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseUri = new Uri("https://localhost:7107/api");
        HttpClient client;
        public CategoriesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseUri;
        }
        public IActionResult Index()
        {
            List<CategoryVM> viewModels = new List<CategoryVM>();
            HttpResponseMessage response = client.GetAsync(baseUri + "/Categories").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                viewModels = JsonConvert.DeserializeObject<List<CategoryVM>>(data);
            }

            return View(viewModels);
            
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVM viewModel)
        {
            string Data = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseUri + "/Categories", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
