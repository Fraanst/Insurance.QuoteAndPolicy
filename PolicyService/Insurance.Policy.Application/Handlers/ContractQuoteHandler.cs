using Insurance.Policy.Application.Commands;
using Insurance.Policy.Domain.Entities;
using Insurance.Policy.Domain.Exceptions;
using Insurance.Policy.Domain.Interfaces.Ports;
using Insurance.Policy.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Insurance.Policy.Application.Handlers;

public class ContractQuoteHandler(ILogger<ContractQuoteHandler> logger, IQuoteServicePort quoteServicePort, IPolicyRepository policyRepository)
{
    public async Task<PolicyEntity> HandleAsync(ContractQuoteCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando contratação da proposta {PropostaId}.", command.QuoteId);

        try
        {
            logger.LogInformation("Iniciando contratação da apólice (Policy) para a Quote {QuoteId}.", command.QuoteId);

            var quote = await quoteServicePort.GetQuoteAsync(command.QuoteId, cancellationToken);

            if (quote.Status == QuoteStatus.Approved)
            {
                logger.LogWarning("A Quote {QuoteId} não está Aprovada e não pode ser contratada.", command.QuoteId);
                throw new QuoteNotApprovedException(command.QuoteId);
            }

            var policy = new PolicyEntity
            {
                Id = Guid.NewGuid(),
                QuoteId = command.QuoteId,
                ContractDate = DateTime.UtcNow,
                PremiumValue = command.PremiumValue
            };

            await policyRepository.ContractQuote(policy, cancellationToken);

            logger.LogInformation("Contrato {ContratoId} criado para a proposta {PropostaId}",
                policy.Id,
                command.QuoteId
            );

            return policy;

        }
        catch (Exception ex)
        {
            logger.LogError("Ocorreu um erro ao tentar contratar proposta proposta:{quoteId} erro: {ex.Message}.", command.QuoteId, ex.Message);
            throw new PolicyException($"Ocorreu um erro ao tentar contratar proposta {command.QuoteId}");

        }
    }
}