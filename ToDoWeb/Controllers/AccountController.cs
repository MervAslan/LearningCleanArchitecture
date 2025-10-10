using Microsoft.AspNetCore.Mvc;
using MediatR;
using ToDo.Application.CQRS.Commands;


namespace ToDoWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                
                TempData["ErrorMessage"] = result.Message;
                return View(command); 
            }

            
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

           
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(command); 
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Dashboard", "Main");
        }
    }
}
