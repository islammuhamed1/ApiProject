using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasktetAsync(string id);
        Task<BasketDto> UpdateBasketAsync(BasketDto basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string id);
    }
}
