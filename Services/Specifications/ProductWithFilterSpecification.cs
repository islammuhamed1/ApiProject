using Domain.Contracts;
using Domain.Entities;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithFilterSpecification : Specification<Product>
    {
        public ProductWithFilterSpecification(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        public ProductWithFilterSpecification(int id, ProductSpecificationParams specs)
            : base(product => product.Id == id &&
                (!specs.BrandId.HasValue || product.BrandId == specs.BrandId) &&
                (!specs.TypeId.HasValue || product.TypeId == specs.TypeId) &&
                (string.IsNullOrWhiteSpace(specs.Search) ||
                 product.Name.ToLower().Contains(specs.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            ApplyPagination(specs.PageIndex, specs.PageSize);
            ApplySorting(specs);
        }

        public ProductWithFilterSpecification(ProductSpecificationParams specs)
            : base(product =>
                (!specs.BrandId.HasValue || product.BrandId == specs.BrandId) &&
                (!specs.TypeId.HasValue || product.TypeId == specs.TypeId) &&
                (string.IsNullOrWhiteSpace(specs.Search) ||
                 product.Name.ToLower().Contains(specs.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            ApplyPagination(specs.PageIndex, specs.PageSize);
            ApplySorting(specs);
        }

        private void ApplySorting(ProductSpecificationParams specs)
        {
            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "nameAsc":
                        SetOrderBy(product => product.Name);
                        break;
                    case "nameDesc":
                        SetOrderByDescending(product => product.Name);
                        break;
                    case "priceAsc":
                        SetOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        SetOrderByDescending(product => product.Price);
                        break;
                    default:
                        SetOrderBy(product => product.Name);
                        break;
                }
            }
            else
            {
                SetOrderBy(product => product.Name);
            }
        }
    }

}

