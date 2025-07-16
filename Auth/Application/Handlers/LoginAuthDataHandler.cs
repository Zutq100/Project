using Auth.Api.Controllers;
using Auth.Application.DTO;
using Auth.Domain.Models;
using Auth.Infrastructure.EFCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Handlers
{
    public record class LoginAuthDataCommand(LoginAuthDataDTO DTO) : IRequest<AuthDataDTO>;
    public class LoginAuthDataHandler : IRequestHandler<LoginAuthDataCommand, AuthDataDTO>
    {
        private readonly UserManager<AuthData> _userManager;
        private readonly SignInManager<AuthData> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly AuthDbContext _authDbContext;

        public LoginAuthDataHandler(UserManager<AuthData> userManager,
            ILogger<AccountController> logger,
            SignInManager<AuthData> signInManager,
            AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _authDbContext = authDbContext;
        }

        public async Task<AuthDataDTO> Handle(LoginAuthDataCommand request, CancellationToken cancellationToken)
        {
            var entity = await _authDbContext.AuthDatas.FirstOrDefaultAsync(x => x.Email == request.DTO.email);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var result = await _signInManager.PasswordSignInAsync(entity, request.DTO.password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                _logger.LogInformation("User logged in.");
            else
                throw new ArgumentException("User not logged in");
           return new AuthDataDTO(entity.Email, entity.UserName);
        }
    }
}
