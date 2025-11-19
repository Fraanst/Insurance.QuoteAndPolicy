using Insurance.Policy.Domain.Dto;

namespace Insurance.Policy.Domain.Interfaces.Ports
{
    public interface IQuoteServicePort
    {
        Task<QuoteDto> GetQuoteAsync(Guid quoteId, CancellationToken cancellationToken);
    }
}
