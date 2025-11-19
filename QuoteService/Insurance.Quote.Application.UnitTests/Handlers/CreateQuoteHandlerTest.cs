using Moq;
using Microsoft.Extensions.Logging;
using Quote.Domain.Interfaces.Repositories;
using Quote.Application.Handlers;
using Insurance.Quote.Application.Commands;
using Insurance.Quote.Domain.Exceptions;
using Quote.Domain.Entities;
using Insurance.Quote.Domain.Interfaces.Repositories;
using Insurance.Quote.Domain.Enums;

namespace Insurance.Quote.Application.UnitTests.Handlers
{
    public class CreateQuoteHandlerTests
    {
        private readonly Mock<IQuoteRepository> _quoteRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        private readonly Mock<ILogger<CreateQuoteHandler>> _loggerMock;
        private readonly CreateQuoteHandler _handler;

        private readonly Guid CustomerId = Guid.NewGuid();
        private readonly Guid ProductId = Guid.NewGuid();
        private readonly CancellationToken CancellationToken = CancellationToken.None;

        public CreateQuoteHandlerTests()
        {
            _quoteRepositoryMock = new Mock<IQuoteRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();

            _loggerMock = new Mock<ILogger<CreateQuoteHandler>>();

            _handler = new CreateQuoteHandler(
                _quoteRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _productRepositoryMock.Object,
                _loggerMock.Object
            );
        }


        [Fact]
        public async Task HandleAsync_ValidCommand_ShouldCreateQuoteAndReturnEntity()
        {
            // ARRANGE
            var command = new CreateQuoteCommand
            {
                CustomerId = CustomerId,
                ProductId = ProductId,
                InsuranceType = "Auto",
                Status = QuoteStatus.Approved,
                EstimatedValue = 5000.00m
            };

            // ACT
            var resultQuote = await _handler.HandleAsync(command, CancellationToken);

            // ASSERT

            Assert.NotNull(resultQuote);
            Assert.NotEqual(Guid.Empty, resultQuote.QuoteId); 

            _quoteRepositoryMock.Verify(
                r => r.CreateAsync(
                    It.Is<QuoteEntity>(q =>
                        q.CustomerId == CustomerId &&
                        q.ProductId == ProductId &&
                        q.EstimatedValue == 5000.00m),
                    CancellationToken),
                Times.Once);

            Assert.Equal(command.CustomerId, resultQuote.CustomerId);
            Assert.Equal(command.EstimatedValue, resultQuote.EstimatedValue);
        }


        [Fact]
        public async Task HandleAsync_WhenRepositoryThrowsException_ShouldCatchAndThrowQuoteException()
        {
            // ARRANGE
            var command = new CreateQuoteCommand
            {
                CustomerId = CustomerId,
                ProductId = ProductId,
                Status = QuoteStatus.UnderReview,
                EstimatedValue = 1000m
            };

            _quoteRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<QuoteEntity>(), CancellationToken))
                .ThrowsAsync(new InvalidOperationException("Erro de conexão simulado."));

            // ACT & ASSERT
            var exception = await Assert.ThrowsAsync<QuoteException>(
                () => _handler.HandleAsync(command, CancellationToken));

            Assert.Contains("Ocorreu um erro ao tentar criar uma proposta", exception.Message);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Erro ao alterar criar uma proposta")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}