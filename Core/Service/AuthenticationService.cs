using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.Authentication;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IAuthenticationService
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
                    Token = await CreateTokenAsync(User)
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
                    Token =await CreateTokenAsync(User),
                };
            else
            {
                var Errors = IsCreatred.Errors.Select(d => d.Description).ToList();
                throw new BadRequestException(Errors);

            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var Credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials);
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
