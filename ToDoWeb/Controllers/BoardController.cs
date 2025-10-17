using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.CQRS.Commands.BoardCommands;


namespace ToDoWeb.Controllers
{
    public class BoardController : Controller
    {
        private readonly IMediator _mediator;
        public BoardController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBoard(CreateBoardCommand command)
        {
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
