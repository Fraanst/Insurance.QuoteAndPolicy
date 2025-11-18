using Insurance.Policy.Domain.Entities;
using Insurance.Policy.Domain.Exceptions;
using Insurance.Policy.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Insurance.Policy.Application.Handlers
{
    public class GetPolicyByIdHandler(ILogger<ContractQuoteHandler> logger, IPolicyRepository policyRepository)
    {

        public async Task<PolicyEntity> HandlerAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Iniciando busca da proposta: {PropostaId}.", id);
                var policy = await policyRepository.GetPolicyById(id, cancellationToken);
                return policy;

            }
            catch(Exception ex)
            {
                logger.LogError($"Ocorreu um erro ao tentar buscar Proposta: {id} erro: {ex.Message}.");
                throw new PolicyException($"Ocorreu um erro ao tentar buscar Proposta: {id}");
            }
        }
    }
}
