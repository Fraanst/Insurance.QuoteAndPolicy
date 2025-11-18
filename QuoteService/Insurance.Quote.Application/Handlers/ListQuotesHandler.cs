using Insurance.Quote.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Application.Handlers
{
    public class ListQuotesHandler(IQuoteRepository quoteRepository, ILogger<ListQuotesHandler> logger)
    {
        public async Task<IEnumerable<QuoteEntity>> HandleAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Iniciando listagem de propostas");
            try
            {
                return await quoteRepository.GetAll(cancellationToken);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Erro ao alterar listar propostas");
                throw new QuoteException($"Ocorreu um erro ao tentar listar propostas");
            }
        }
    }
}
