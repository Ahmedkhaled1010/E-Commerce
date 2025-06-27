using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.DataTransferObject.IdentityDto;

namespace ServicesImplemetation
{
    public class AuthenticationServices (UserManager<ApplicationUser> userManager,IConfiguration configuration,
        
        IMapper mapper): IAuthenticationServices
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
           
            var user =await userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
          var user =await userManager.Users.Include(u=>u.Adress).FirstOrDefaultAsync(u=>u.Email ==email) ??
                throw new UserNotFoundException(email)
                ;
            return mapper.Map<Address, AddressDto>(user.Adress);
                //if (user.Adress is not null)
                //{
                //    return mapper.Map<Address,AddressDto>(user.Adress) ;    
                //}
                //else
                //{
                //    throw new AddressNotFoundException(user.UserName);
                //}
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await userManager.Users.Include(u => u.Adress).FirstOrDefaultAsync(u => u.Email == email) ??
                throw new UserNotFoundException(email);
            if (user.Adress is not null)
            {
                user.Adress.FirstName = addressDto.FirstName;
                user.Adress.LastName = addressDto.LastName;
                user.Adress.City = addressDto.City;
                user.Adress.Country = addressDto.Country;
                user.Adress.Street = addressDto.Street;
            }
            else
            {
                 user.Adress= mapper.Map<AddressDto,Address>(addressDto) ;
            }
          await  userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(user.Adress);
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user =await userManager.FindByEmailAsync(email)??throw new UserNotFoundException (email);
           return new UserDto() { DisplayName=user.DisplayName,Email=user.Email,Token=await CreateTokenAsync(user)};
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
           var user =await userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
           

            var IsPasswordValid =await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }

        }

       

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,

            };
            var Result =await userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }


        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim (ClaimTypes.Email, user.Email!),
                new Claim (ClaimTypes.Name,user.UserName!),
                new Claim (ClaimTypes.NameIdentifier,user.Id!),

            };
            var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles) 
            {
                claims.Add( new Claim(ClaimTypes.Role, role) );
            }
            var secretkey = configuration.GetSection("JWTOptions")["SecretKey"];
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(

                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(Token); 


        }
    }
}
