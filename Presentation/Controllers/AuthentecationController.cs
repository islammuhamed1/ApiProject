using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthentecationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        => Ok(await serviceManager.AuthentactionService.LoginAsync(loginDto));
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
       => Ok(await serviceManager.AuthentactionService.RegisterAsync(registerDto));
        [HttpGet("GetUserByEmail")]
        [Authorize]
        public async Task<ActionResult<UserResultDto>> GetUserByEmail()
        {
            var email = User.FindFirst(x => x.Type == "email")?.Value;
            var result = await serviceManager.AuthentactionService.GetUserByEmailAsync(email);
            return Ok(result);
        }
        [HttpGet("IsEmailExist")]
        public async Task<ActionResult<bool>> IsEmailExist(string email)
        => Ok(await serviceManager.AuthentactionService.IsEmailExist(email));
        [HttpGet("GetUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        { 
            var email = User.FindFirst(x => x.Type == "email")?.Value;
            var result = await serviceManager.AuthentactionService.GetUserAddressAsync(email);
            return Ok(result);
        }
        [HttpPut("UpdateUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirst(x => x.Type == "email")?.Value;
            var result = await serviceManager.AuthentactionService.UpdateUserAddresAsync(email,addressDto);
            return Ok(result);
        }
    }
}
