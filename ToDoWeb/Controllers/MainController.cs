using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Interfaces;
using static ToDo.Application.CQRS.Queries.UserQueries.DashboardQuery;

namespace ToDoWeb.Controllers
{
    public class MainController : Controller
    {

        private readonly IMediator _mediator;

        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Dashboard()
        {
            
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            
            var query = new GetUserDashboardQuery { Email = email }; //bu emaile ait kullanıcı verilerini getircez
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Login", "Account");
            }

           
            return View(result.Data);
        }

    }
}
