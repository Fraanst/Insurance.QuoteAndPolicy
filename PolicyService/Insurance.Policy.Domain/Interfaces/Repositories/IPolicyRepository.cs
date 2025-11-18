using Insurance.Policy.Domain.Entities;

namespace Insurance.Policy.Domain.Interfaces.Repositories
{
    public interface IPolicyRepository
    {
        Task<PolicyEntity> ContractQuote(PolicyEntity policy, CancellationToken cancellationToken = default);
        Task<PolicyEntity> GetPolicyById(Guid policyId, CancellationToken cancellationToken = default);
    }
}
