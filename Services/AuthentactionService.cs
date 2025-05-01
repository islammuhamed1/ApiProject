using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthentactionService(UserManager<User> userManager ,
        IMapper mapper,
        IOptions<JwtOption>options)
        : IAuthentactionService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new UnAuthorizedException($"Email {loginDto.Email} does not Exist!");
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new UnAuthorizedException($"Password is incorrect!");
            return new UserResultDto
            (
               user.DisplayName,
               user.Email,
              await CreateTokenAsync(user)

            );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto loginDto)
        {
            var user = new User
            {
                Email = loginDto.Email,
                UserName = loginDto.UserName,
                DisplayName = loginDto.DisplayName,
                PhoneNumber = loginDto.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, loginDto.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x=>x.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto
            (
               user.DisplayName,
               user.Email,
              await CreateTokenAsync(user)

            );
        }
        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOption = options.Value;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("top-secret-key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: jwtOption.Issuer,
                audience: jwtOption.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOption.DurationInDays),
                signingCredentials: creds
                
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
