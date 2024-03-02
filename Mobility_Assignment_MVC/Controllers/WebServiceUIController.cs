using Microsoft.AspNetCore.Mvc;
using Mobility_Assignment_MVC.Models.Data;

namespace Mobility_Assignment_MVC.Controllers
{
    public class WebServiceUIController : Controller
    {
        private readonly WebServiceDbContext _webServiceDbContext;
        public WebServiceUIController(WebServiceDbContext webServiceDbContext)
        {
            _webServiceDbContext = webServiceDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
