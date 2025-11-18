using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Quote.Application.Handlers
{
    public class CreateQuoteHandler
    {
        private readonly IQuoteRepository _quoteRepository;

        public CreateQuoteHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<QuoteEntity> HandleAsync(
            Guid customerId,
            Guid productId,
            string? insuranceType,
            QuoteStatus status,
            decimal estimatedValue,
            CancellationToken cancellationToken)
        {
            var quote = new QuoteEntity
            {
                QuoteId = Guid.NewGuid(),
                CustomerId = customerId,
                ProductId = productId,
                InsuranceType = insuranceType,
                Status = status,
                EstimatedValue = estimatedValue,
                CreatedAt = DateTime.UtcNow
            };

            await _quoteRepository.CreateAsync(quote, cancellationToken);

            return quote;
        }
    }
}
