using Auth.Application.DTO;
using Auth.Application.Handlers;
using Auth.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;
        private readonly SignInManager<AuthData> _signInManager;

        public AccountController(ILogger<AccountController> logger, IMediator mediator, SignInManager<AuthData> signInManager)
        {
            _logger = logger;
            _mediator = mediator;
            _signInManager = signInManager;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterAuthDataDTO DTO, CancellationToken cancellationToken)
        {
            var query = new CreateAuthDataCommand(DTO);
            var entity = await _mediator.Send(query, cancellationToken);

            return Ok(entity);
        }

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            return Redirect(returnUrl);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAuthDataDTO DTO, CancellationToken cancellationToken , string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var query = new LoginAuthDataCommand(DTO);
            var entity = await _mediator.Send(query, cancellationToken);

            return Redirect(returnUrl);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Auth", "Register");
        }
    }
}
