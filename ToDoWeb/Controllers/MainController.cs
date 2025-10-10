using Microsoft.AspNetCore.Mvc;

namespace ToDoWeb.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
