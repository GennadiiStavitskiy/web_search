using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private string apiBaseUrl = "http://localhost:59022/api/search";        
        HttpClient client;

        public HomeController()
        {            
            this.client = new HttpClient();
        }
        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(actionName: nameof(Index));
            }

            var url = $"{apiBaseUrl}/{model.Engine}?url={model.Url}&text={model.Text}";

            var responce = await this.client.GetAsync(url);

            model.Result = await responce.Content.ReadAsStringAsync();

            return View(model);
        }
    }
}
