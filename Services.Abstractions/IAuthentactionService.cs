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

    }
}
