using Quote.Domain.Entities;

namespace Quote.Domain.Interfaces.Repositories;
    public interface IQuoteRepository
    {
        Task CreateAsync(QuoteEntity quote, CancellationToken cancellationToken = default);
        Task UpdateStatusAsync(Guid quoteId, QuoteStatus status, CancellationToken cancellationToken = default)
        Task<QuoteEntity?> GetByIdAsync(Guid quoteId, CancellationToken cancellationToken = default);
        Task<IEnumerable<QuoteEntity>> GetByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<QuoteEntity>> GetAll(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid quoteId, CancellationToken cancellationToken = default);
    }
