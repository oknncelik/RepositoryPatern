using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstruct
{
    public interface IProductManager
    {
        Task<ProductModel> GetProduct(int id);
        Task<bool> DeleteProduct(ProductModel product);
        Task<IList<ProductModel>> GetProducts(ProductFilter filter);
        Task<ProductModel> AddProducts(ProductModel product);
    }
}