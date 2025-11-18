using Quote.Domain.Interfaces.Repositories;

public class ChangeQuoteStatusHandler(IQuoteRepository quoteRepository)
{
    public async Task HandleAsync(Guid quoteId, QuoteStatus newStatus, CancellationToken cancellationToken)
    {
        var quote = await quoteRepository.GetByIdAsync(quoteId, cancellationToken);

        if (quote is null)
            throw new KeyNotFoundException("Proposta não encontrada");

        if (!quote.CanChangeStatusTo(newStatus))
            throw new InvalidOperationException(
                $"Não é possível alterar o status da proposta de {quote.Status} para {newStatus}.");

        quote.ChangeStatus(newStatus);

        await quoteRepository.UpdateAsync(quote, cancellationToken);
    }
}
