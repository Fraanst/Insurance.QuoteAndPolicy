using Insurance.Quote.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

public class GetQuoteByIdHandler(IQuoteRepository quoteRepository, ILogger<GetQuoteByIdHandler> logger)
{
    public async Task<QuoteEntity?> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Iniciando busca da proposta {id}");

        try
        {
            return await quoteRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erro ao alterar buscar uma proposta");
            throw new QuoteException($"Ocorreu um erro ao tentar buscar uma proposta");
        }
    }
}
