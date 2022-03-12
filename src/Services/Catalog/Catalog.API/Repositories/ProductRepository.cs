using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<Product> GetProductAsync(string id)
            => await _catalogContext.Products
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
        public async Task CreateProductAsync(Product product)
            => await _catalogContext.Products.InsertOneAsync(product);

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
            => await _catalogContext.Products
            .Find(Builders<Product>.Filter.Eq(p => p.Category, categoryName))
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
            => await _catalogContext.Products
            .Find(Builders<Product>.Filter.Eq(p => p.Name, name))
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsAsync()
            => await _catalogContext.Products.Find(x => true).ToListAsync();

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var res = await _catalogContext
                .Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var res = await _catalogContext.Products.DeleteOneAsync(x => x.Id == id);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }
    }
}
