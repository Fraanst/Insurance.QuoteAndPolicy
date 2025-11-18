using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        public Task CreateAsync(QuoteEntity quote, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuoteEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<QuoteEntity?> GetByIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusAsync(QuoteEntity quote, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
