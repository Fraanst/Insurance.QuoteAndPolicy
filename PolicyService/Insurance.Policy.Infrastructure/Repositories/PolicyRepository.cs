using Insurance.Policy.Domain.Entities;
using Insurance.Policy.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Policy.Infrastructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        public Task<PolicyEntity> ContractQuote(PolicyEntity policy, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyEntity> GetPolicyById(Guid policyId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
