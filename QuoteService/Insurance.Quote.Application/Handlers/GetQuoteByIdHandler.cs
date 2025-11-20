using Insurance.Quote.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

public class GetQuoteByIdHandler(IQuoteRepository quoteRepository, ILogger<GetQuoteByIdHandler> logger)
{
    public async Task<QuoteEntity?> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Iniciando busca da proposta {id}");
        var quote =  await quoteRepository.GetByIdAsync(id, cancellationToken);
        if(quote == null)
        {
            logger.LogWarning($"Proposta {id} não encontrada");
            throw new QuoteNotFoundException();
        }
        return quote;
    }
}
