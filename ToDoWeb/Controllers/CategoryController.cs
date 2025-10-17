using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.CQRS.Commands.CategoryCommands;
using ToDo.Application.Interfaces;
namespace ToDoWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public CategoryController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var userId = _currentUserService.CurrentUserId;
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login", "Account");
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Boards", "Main");
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Boards", "Main");


        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            var userId = _currentUserService.CurrentUserId;
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Boards", "Main");
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Boards", "Main");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            var userId = _currentUserService.CurrentUserId;
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login", "Account");
            }
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Boards", "Main");
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Boards", "Main");
        }
        

    }
}
