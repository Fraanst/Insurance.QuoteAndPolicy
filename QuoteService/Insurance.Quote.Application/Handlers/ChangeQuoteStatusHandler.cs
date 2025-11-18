using Insurance.Quote.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Quote.Domain.Interfaces.Repositories;

public class ChangeQuoteStatusHandler(
        IQuoteRepository quoteRepository,
        ILogger<ChangeQuoteStatusHandler> logger)
{
    public async Task HandleAsync(Guid quoteId, QuoteStatus newStatus, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Alterando status da proposta:{quoteId} para {Enum.GetName(newStatus)}");

        try
        {
            var quote = await quoteRepository.GetByIdAsync(quoteId, cancellationToken);

            if (quote is null)
                throw new KeyNotFoundException("Proposta não encontrada");

            //if (!quote.CanChangeStatusTo(newStatus))
            //    throw new QuoteStatusChangeFailedException(quoteId, Enum.GetName(newStatus));

            //quote.ChangeStatus(newStatus);

            await quoteRepository.UpdateStatusAsync(quote, cancellationToken);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erro ao alterar status da proposta:{quoteId} para {newStatus}");
            throw new QuoteException($"Ocorreu um erro ao tentar alterar status da proposta: {quoteId} para {Enum.GetName(newStatus)}");
        }
    }
}
