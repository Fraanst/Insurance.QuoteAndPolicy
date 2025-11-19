using Insurance.Quote.Domain.Interfaces.Repositories;
using Quote.Domain.Entities;

namespace Insurance.Quote.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly QuoteContext _context;

        public ProductRepository(QuoteContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ProductEntity product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
