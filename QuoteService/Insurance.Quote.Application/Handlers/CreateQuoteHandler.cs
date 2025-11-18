using Insurance.Quote.Application.Commands;
using Insurance.Quote.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Quote.Application.Handlers
{
    public class CreateQuoteHandler(
        IQuoteRepository quoteRepository,
        ILogger<CreateQuoteHandler> logger)
    {

        public async Task<QuoteEntity> HandleAsync(CreateQuoteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Iniciando criação da proposta");

                var quote = new QuoteEntity
                {
                    QuoteId = Guid.NewGuid(),
                    CustomerId = command.CustomerId,
                    ProductId = command.ProductId,
                    InsuranceType = command.InsuranceType,
                    Status = command.Status,
                    EstimatedValue = command.EstimatedValue,
                    CreatedAt = DateTime.UtcNow
                };

                await quoteRepository.CreateAsync(quote, cancellationToken);

                return quote;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Erro ao alterar criar uma proposta");
                throw new QuoteException($"Ocorreu um erro ao tentar criar uma proposta");
            }
        }
    }
}
