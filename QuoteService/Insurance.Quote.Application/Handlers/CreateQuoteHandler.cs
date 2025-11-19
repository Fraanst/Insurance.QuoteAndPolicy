using Insurance.Quote.Application.Commands;
using Insurance.Quote.Domain.Exceptions;
using Insurance.Quote.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Quote.Application.Handlers
{
    public class CreateQuoteHandler(
        IQuoteRepository quoteRepository,
        ICustomerRepository customerRepository,
        IProductRepository ProductRepository,
        ILogger<CreateQuoteHandler> logger)
    {

        public async Task<QuoteEntity> HandleAsync(CreateQuoteCommand command, CancellationToken cancellationToken)
        {
            try
            {

                logger.LogInformation("Iniciando criação da proposta");

                #region criando uma estrutura de customer e product para testes
                var customer = new CustomerEntity
                {
                    CustomerId = command.CustomerId,
                    CustomerName = "Francine Stramantinoli",
                    DocumentNumber = "123.456.789-00",
                    BirthDate = new DateTime(1993, 04, 12).ToShortDateString()
                };
                await customerRepository.CreateAsync(customer, cancellationToken);

                var product = new ProductEntity
                {
                    ProductId = command.ProductId,
                    ProductType = "Auto",
                    Value = 100000.00m
                };
                
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
                await ProductRepository.CreateAsync(product, cancellationToken);
                #endregion

                await quoteRepository.CreateAsync(quote, cancellationToken);

                return quote;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro ao alterar criar uma proposta");
                throw new QuoteException($"Ocorreu um erro ao tentar criar uma proposta");
            }
        }
    }
}
