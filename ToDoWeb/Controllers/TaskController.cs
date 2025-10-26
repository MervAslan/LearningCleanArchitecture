using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.CQRS.Commands.TaskCommands;

namespace ToDoWeb.Controllers
{
    public class TaskController : Controller
    {
        private readonly IMediator _mediator;
        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Boards", "Main");
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Boards", "Main", new { boardId = command.BoardId, taskId = result.Data.Id });
        }
    }
}
