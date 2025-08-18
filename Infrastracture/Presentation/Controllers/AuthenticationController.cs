using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstrastion;
using Shared.DataTransfereObjects.Authentication;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.Authentication.LoginAsync(loginDto);
            return Ok(User);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.Authentication.RegisterAsync(registerDto);
            return Ok(User);
        }


        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _serviceManager.Authentication.CheckEmailAsync(email);
            return Ok(Result);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var CurrentUser = await _serviceManager.Authentication.GetCurrentUserAsync(Email!);
            return Ok(CurrentUser);
        }

        [Authorize]
        [HttpGet("CurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAdderss()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var CurrentUserAddress = await _serviceManager.Authentication.GetCurrentUserAdderssAsync(Email!);
            return Ok(CurrentUserAddress);
        }

        [Authorize]
        [HttpPut("UpdateCurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto address)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await _serviceManager.Authentication.UpdateCurrentUserAddressAsync(address, Email!);
            return Ok(UpdatedAddress);
        }

    }
}
