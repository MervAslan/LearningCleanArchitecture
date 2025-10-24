using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.CQRS.Commands.BoardCommands;
using ToDo.Domain.Entities;


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
            return RedirectToAction("Boards", "Main", new { categoryId = command.CategoryId, boardId = result.Data.Id });


        }
        [HttpPost]
        public async Task<IActionResult> DeleteBoard(DeleteBoardCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            else
            {
                TempData["SuccessMessage"] = result.Message;
            }
            return RedirectToAction("Boards", "Main");

        }
        [HttpPost]
        public async Task<IActionResult> UpdateBoard(UpdateBoardCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            else
            {
                TempData["SuccessMessage"] = result.Message;
            }
            return RedirectToAction("Boards", "Main");
        }
    }
}
