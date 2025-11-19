using Insurance.Quote.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly QuoteContext _context;
       
        public QuoteRepository(QuoteContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(QuoteEntity quote, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Quotes.AddAsync(quote, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, $"Erro ao alterar criar uma proposta");
                throw new QuoteException($"Ocorreu um erro ao tentar criar proposta no banco");
            }
        }

        public async Task DeleteAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var quoteToDelete = new QuoteEntity { QuoteId = quoteId };

            _context.Quotes.Attach(quoteToDelete);
            _context.Quotes.Remove(quoteToDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<QuoteEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Quotes
                                  .AsNoTracking()
                                  .Include(x => x.Customer)
                                  .Include(x => x.Product)
                                  .ToListAsync(cancellationToken);
        }

        public async  Task<QuoteEntity?> GetByIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            return await _context.Quotes
                                 .AsNoTracking()
                                 .Include(x => x.Customer)
                                 .Include(x => x.Product)
                                 .FirstOrDefaultAsync(q => q.QuoteId == quoteId, cancellationToken);
        }

        public async Task UpdateStatusAsync(QuoteEntity quote, CancellationToken cancellationToken = default)
        { 
            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
