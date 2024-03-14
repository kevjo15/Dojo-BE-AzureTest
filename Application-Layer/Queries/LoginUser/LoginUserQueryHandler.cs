using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Models.UserModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application_Layer.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResult>
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        public LoginUserQueryHandler(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<LoginResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email!);
            if (user == null)
            {
                return CreateLoginResult(false, "User not found.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            return CreateLoginResult(result.Succeeded, result.Succeeded ? null : "Invalid login attempt.");
        }

        private LoginResult CreateLoginResult(bool successful, string? error)
        {
            return new LoginResult { Successful = successful, Error = error };
        }
    }
}
