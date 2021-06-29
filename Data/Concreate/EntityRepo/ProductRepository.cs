using Data.Abstruct.EntityRepo;
using Entities.Entities;

namespace Data.Concreate.EntityRepo
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
    }
}