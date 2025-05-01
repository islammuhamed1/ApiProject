using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    { 
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthentactionService> _authentactionService;

        public ServiceManager(IUnitOfWork unitOfWork ,
            IMapper mapper ,
            IBasketRepository basketRepository ,
            UserManager<User> userManager,
            IOptions<JwtOption> options)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
            _authentactionService = new Lazy<IAuthentactionService>(() => new AuthentactionService(userManager, mapper, options));
        }
        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;

        public IAuthentactionService AuthentactionService => _authentactionService.Value;
    }
}
