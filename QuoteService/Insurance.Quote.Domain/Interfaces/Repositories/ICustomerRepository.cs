using Quote.Domain.Entities;

namespace Insurance.Quote.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateAsync(CustomerEntity customer, CancellationToken cancellationToken = default);
    }
}
