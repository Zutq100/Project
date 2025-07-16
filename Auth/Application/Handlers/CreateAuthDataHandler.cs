using Auth.Application.DTO;
using Auth.Domain.Models;
using Auth.Infrastructure.EFCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Auth.Application.Handlers
{
    public record CreateAuthDataCommand(RegisterAuthDataDTO DTO) : IRequest<string>;
    public class CreateAuthDataHandler : IRequestHandler<CreateAuthDataCommand, string>
    {
        private readonly UserManager<AuthData> _userManager;
        private readonly SignInManager<AuthData> _signInManager;
        public CreateAuthDataHandler(SignInManager<AuthData> signInManager, UserManager<AuthData> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<string> Handle(CreateAuthDataCommand request, CancellationToken cancellationToken)
        {
            var user = new AuthData { UserName = request.DTO.nickName, Email = request.DTO.email };
            var result = await _userManager.CreateAsync(user, request.DTO.password);
            
            if(result.Succeeded)
                await _signInManager.SignInAsync(user, false);
            
            return user.Id;
        }
    }

}
