using Microsoft.AspNetCore.Mvc;

namespace BookCart.Areas.tktnq.Controllers
{
    public class HomeController : Controller
    {
        [Area("tktnq")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
