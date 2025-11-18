using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Application.Handlers
{
    public class ListQuotesHandler(IQuoteRepository quoteRepository)
    {
        public async Task<IEnumerable<QuoteEntity>> HandleAsync(CancellationToken cancellationToken)
        {
            return await quoteRepository.GetAll(cancellationToken);
        }
    }
}
