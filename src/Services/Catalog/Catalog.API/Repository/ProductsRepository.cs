using Catalog.API.Data;
using Catalog.API.Entities.Domain;
using Catalog.API.Repository.IRepository;

namespace Catalog.API.Repository
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        private readonly CatalogContext _db;
        public ProductsRepository(CatalogContext db) : base(db)
        {
            _db = db;
        }
    }
}
