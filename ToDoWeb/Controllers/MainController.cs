using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Interfaces;
using static ToDo.Application.CQRS.Queries.DashboardQueries.DashboardQuery;


namespace ToDoWeb.Controllers
{
    public class MainController : Controller
    {


        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public MainController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Boards()
        {
            int currentUserId;
            try
            {
                currentUserId = _currentUserService.CurrentUserId;
            }
            catch
            {
                TempData["ErrorMessage"] = "Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            var query = new GetUserDashboardQuery();  // yeni instance oluşturduk bunu oluşturmamızın sebebi parametresiz olması. 
            var result = await _mediator.Send(query); // parametresiz olmasının sebebi ise herhangi bir bind işleminin olmaması. 

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Login", "Account");
            }

            return View(result.Data);
        }



    }
}
