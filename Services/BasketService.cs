using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
        => await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto> GetBasktetAsync(string id)
        {
            var  basket = await basketRepository.GetBasketAsync(id);

            return basket is null ? throw new BasketNotFoundException(id)
                                    : mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketDto basket, TimeSpan? timeToLive = null)
        {
            var cutomerBasket = mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await basketRepository.UpdateBasketAsync(cutomerBasket);
            return updatedBasket is null ? throw new Exception("Cannot Updaded Right Now")
                                    : mapper.Map<BasketDto>(updatedBasket);
        }
    }
}
