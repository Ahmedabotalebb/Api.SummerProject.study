using System;
using System.Collections.Generic;
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
    }
}
