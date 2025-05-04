using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthentactionService
    {
        Task<UserResultDto>LoginAsync(LoginDto loginDto);
        Task<UserResultDto>RegisterAsync(RegisterDto loginDto);
        Task<UserResultDto>GetUserByEmailAsync(string email);
        Task<bool>IsEmailExist(string email);
        Task<AddressDto> GetUserAddressAsync(string email);
        Task<AddressDto> UpdateUserAddresAsync(string email, AddressDto addressDto);


    }
}
