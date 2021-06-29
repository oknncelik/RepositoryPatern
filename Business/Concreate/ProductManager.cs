using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Abstruct;
using Common;
using Data.Abstruct.EntityRepo;
using Data.Concreate.EntityRepo;
using Data.Models;
using Entities.Dtos;
using Entities.Entities;

namespace Business.Concreate
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager()
        {
            _productRepository = new ProductRepository();
        }
        
        public async Task<IList<ProductModel>> GetProducts(ProductFilter filter)
        {
            var queryFilter = new List<Expression<Func<Product, bool>>>();
            
            if (filter.Name.IsNotNullOrEmpty())
                queryFilter.Add(x=> x.Name.Contains(filter.Name));

            if (filter.Description.IsNotNullOrEmpty())
                queryFilter.Add(x=> x.Description.Contains(filter.Description));

            var orderByFilter = new OrderByExpression<Product>()
            {
                Predicate = x => x.CategoryId,
                Direction = OrderBy.desc,
                PredicateThenBy = new ThenByExpression<Product>
                {
                    Predicate = x=> x.Description,
                    Direction = OrderBy.asc
                }
            };

            var result = await _productRepository.GetList(queryFilter.ToArray(), orderByFilter);
            return result.Select(DataToEntity).ToList();

        }

        public async Task<ProductModel> AddProducts(ProductModel product)
        {
            var result = await _productRepository.Add(new Product()
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ActiveFlg = true
            });

            return DataToEntity(result);
        }

        private ProductModel DataToEntity(Product arg)
        {
            return new ProductModel()
            {
                Id = arg.Id,
                Name = arg.Name,
                Description = arg.Description,
                CategoryId = arg.CategoryId,
                ActiveFlg = arg.ActiveFlg,
                Price = arg.Price
            };
        }
    }
}