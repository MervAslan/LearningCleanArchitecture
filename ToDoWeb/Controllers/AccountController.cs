using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.CQRS.Commands.UserCommands;
using ToDo.Application.CQRS.Queries.UserQueries;



namespace ToDoWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
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

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserQuery query)
        {
            if (!ModelState.IsValid)
            {
                return View(query);
            }

           
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(query); 
            }
            var user = result.Data;
            var claims = new List<Claim> //kullanıcı bilgilerini tutan claimler
            {
                 new Claim("UserId", user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.Username ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            
            await HttpContext.SignInAsync(  //cookie ile oturum 
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                });

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Boards", "Main");
        }
    }
}
