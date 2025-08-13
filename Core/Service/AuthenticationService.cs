using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.Authentication;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null ;
        }

        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email)?? throw new UserNotFoundException(Email);
            return new UserDto { DisplayName = User.DisplayName, Email = Email ,Token = await CreateTokenAsync(User)};
            
        }

        public async Task<AddressDto> GetCurrentUserAdderssAsync(string email)
        {
            var User =await _userManager.Users.Include(U=>U.Address).
                FirstOrDefaultAsync(U=>U.Email==email)??throw new UserNotFoundException(email);
            if (User.Address is not null)
               return  _mapper.Map<Address, AddressDto>(User.Address);
            else
                throw new AddressNotFoundException(email) ;
        }


        public async Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto UpdatedaddressDto, string email)
        {
            var User = await _userManager.Users.Include(U => U.Address).
                FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
            {
                User.Address.firstName = UpdatedaddressDto.firstName;
                User.Address.LastName = UpdatedaddressDto.LastName;
                User.Address.Country = UpdatedaddressDto.Country;
                User.Address.City = UpdatedaddressDto.City;
                User.Address.Streat = UpdatedaddressDto.Streat;

            }
            else
                _mapper.Map<AddressDto, Address>(UpdatedaddressDto);
            await _userManager.UpdateAsync(User);

            return _mapper.Map<AddressDto>(User.Address);
        }

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
