using Insurance.Quote.Domain.Interfaces.Repositories;
using Quote.Domain.Entities;

namespace Insurance.Quote.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly QuoteContext _context;

        public CustomerRepository(QuoteContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CustomerEntity customer, CancellationToken cancellationToken = default)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
