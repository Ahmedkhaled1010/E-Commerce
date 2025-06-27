using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObject.IdentityDto;

namespace Presentation.Controller
{
    public class AuthenticationController(IServicesManager services):ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await services.AuthenticationServices.LoginAsync(loginDto);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
        { 
            var user = await services.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string email) 
        { 
            var res =await services.AuthenticationServices.CheckEmailAsync(email);
            return Ok(res);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Appuser=await services.AuthenticationServices.GetCurrentUserAsync(email!);
            return Ok(Appuser);
        }
        [Authorize]
        [HttpGet("Address")]

        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await services.AuthenticationServices.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdateAddress = await services.AuthenticationServices.UpdateCurrentUserAddressAsync(email!,address);
            return Ok(UpdateAddress);
        }

    } 
}
