using Quote.Domain.Entities;

namespace Insurance.Quote.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductEntity product, CancellationToken cancellationToken = default);
    }
}
