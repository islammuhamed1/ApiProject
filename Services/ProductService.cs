using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParams specifications)
        {
            var specs = new ProductWithFilterSpecification(specifications);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            var countSpecs = new ProductCountSpecification(specifications);
            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpecs);
            var mappedProducts = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginatedResult<ProductResultDto>
                (specifications.PageIndex, specifications.PageSize,mappedProducts.Count(),mappedProducts);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var mappedtypes = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return mappedtypes;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithFilterSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = mapper.Map <ProductResultDto> (product);
            return mappedProduct;
        }
    }
}

