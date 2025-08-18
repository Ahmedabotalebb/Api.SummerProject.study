using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransfereObjects.Authentication;

namespace ServiceAbstrastion
{
    public interface IAuthenticationService
    {
        public Task<UserDto> LoginAsync(LoginDto loginDto);

        public Task<UserDto> RegisterAsync(RegisterDto registerDto);


        //Check Email Endpoint
        //Take Email Then Return boolean
        public Task<bool> CheckEmailAsync(string email);

        //Get Current User Address
        //Take Email Then Return Address of Current Logged in User
        public Task<AddressDto> GetCurrentUserAdderssAsync(string email);


        //Update Current User Address
        //Take Updated Address and Email Then Return Address after Update
        public Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto UpdatedaddressDto ,string email);

        //Get Current User
        //Take Email Then Return Token , Email and Display Name

        public Task<UserDto> GetCurrentUserAsync(string Email);
    }
}
