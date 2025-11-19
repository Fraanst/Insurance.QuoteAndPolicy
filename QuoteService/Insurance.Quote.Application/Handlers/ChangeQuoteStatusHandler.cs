using Insurance.Quote.Domain.Exceptions;
using Insurance.Quote.Domain.Interfaces.Ports;
using Microsoft.Extensions.Logging;
using Quote.Domain.Interfaces.Repositories;

public class ChangeQuoteStatusHandler(
        IQuoteRepository quoteRepository,
        ILogger<ChangeQuoteStatusHandler> logger,
        IQuoteNotificationPort quoteNotificationPort)
{
    public async Task HandleAsync(Guid quoteId, QuoteStatus newStatus, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Alterando status da proposta:{quoteId} para {Enum.GetName(newStatus)}");

        var quote = await quoteRepository.GetByIdAsync(quoteId, cancellationToken);

        if (quote is null)
        {
            logger.LogError($"Proposta não encontrada:{quoteId}");
            throw new KeyNotFoundException("Proposta não encontrada");
        }

        if (!quote.CanChangeStatusTo(newStatus))
        {
            logger.LogError($"Proposta não estava como pendente:{quoteId} para {newStatus}");
            throw new QuoteStatusChangeFailedException(quoteId, Enum.GetName(newStatus));
        }

        quote.ChangeStatus(newStatus);

        await quoteRepository.UpdateStatusAsync(quote, cancellationToken);

        if(quote.Status == QuoteStatus.Approved)
            await quoteNotificationPort.NotifyQuoteApprovedAsync(quote.QuoteId, cancellationToken);

    }
}
