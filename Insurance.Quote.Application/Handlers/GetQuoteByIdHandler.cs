using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

public class GetQuoteByIdHandler(IQuoteRepository quoteRepository)
{
    public async Task<QuoteEntity?> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        return await quoteRepository.GetByIdAsync(id, cancellationToken);
    }
}
