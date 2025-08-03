using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.Authentication;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto
                {
                    Email = loginDto.Email,
                    DisplayName = User.DisplayName,
                    Token = CreateTokenAsync()
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }
        
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var IsCreatred = await _userManager.CreateAsync(User, registerDto.Password);
            if (IsCreatred.Succeeded)
                return new UserDto
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Token = CreateTokenAsync(),
                };
            else
            {
                var Errors = IsCreatred.Errors.Select(d => d.Description).ToList();
                throw new BadRequestException(Errors);

            }
        }

        private static string CreateTokenAsync()
        {
            return "";
        }
    }
}
