using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.CQRS.Queries.DashboardQueries;
using ToDo.Application.Interfaces;


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

            var query = new DashboardQuery.GetUserDashboardQuery(); // yeni instance
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
