using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Abstruct;
using Common;
using Common.Cache;
using Data.Abstruct.EntityRepo;
using Data.Concreate.EntityRepo;
using Data.Models;
using Entities.Dtos;
using Entities.Entities;
using Newtonsoft.Json;

namespace Business.Concreate
{
   public class ProductManager : IProductManager
   {
      private readonly IProductRepository _productRepository;
      private readonly IRedisCache _redisCache;
      public ProductManager(IRedisCache redisCache)
      {
         _productRepository = new ProductRepository();
         _redisCache = redisCache;
      }

      public async Task<ProductModel> GetProduct(int id)
      {
         var result = await _productRepository.Get(x => x.Id == id);
         return EntityToModel(result);
      }

      public async Task<bool> DeleteProduct(ProductModel product)
      {
         return await _productRepository.Delete(ModelToEntity(product));
      }

      public async Task<IList<ProductModel>> GetProducts(ProductFilter filter)
      {
         var cache = await _redisCache.GetCacheAsync<IList<ProductModel>>(JsonConvert.SerializeObject(filter));
         if (cache != null)
            return cache;

         var queryFilter = new List<Expression<Func<Product, bool>>>();

         if (filter.Name.IsNotNullOrEmpty())
            queryFilter.Add(x => x.Name.Contains(filter.Name));

         if (filter.Description.IsNotNullOrEmpty())
            queryFilter.Add(x => x.Description.Contains(filter.Description));

         var orderByFilter = new OrderByExpression<Product>()
         {
            Predicate = x => x.CategoryId,
            Direction = OrderBy.desc,
            PredicateThenBy = new ThenByExpression<Product>
            {
               Predicate = x => x.Description,
               Direction = OrderBy.asc
            }
         };

         var repositoryResult = await _productRepository.GetList(queryFilter.ToArray(), orderByFilter);
         var result = repositoryResult.Select(EntityToModel).ToList();
         await _redisCache.SetCacheAsync<IList<ProductModel>>(JsonConvert.SerializeObject(filter), result);
         return result;

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

         return EntityToModel(result);
      }

      private ProductModel EntityToModel(Product arg)
      {
         if (arg == null)
            return null;
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

      private Product ModelToEntity(ProductModel arg)
      {
         if (arg == null)
            return null;
         return new Product()
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