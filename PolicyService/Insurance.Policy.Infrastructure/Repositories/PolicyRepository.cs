using Insurance.Policy.Domain.Entities;
using Insurance.Policy.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Policy.Infrastructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly PolicyDbContext _context;

        public PolicyRepository(PolicyDbContext context)
        {
            _context = context;
        }

        public async Task<PolicyEntity> ContractQuote(PolicyEntity policy, CancellationToken cancellationToken = default)
        {
            await _context.Policy.AddAsync(policy, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return policy;
        }

        public async Task<PolicyEntity> GetPolicyById(Guid policyId, CancellationToken cancellationToken = default)
        {
            return await _context.Policy
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(q => q.Id == policyId, cancellationToken);
        }
    }
}
