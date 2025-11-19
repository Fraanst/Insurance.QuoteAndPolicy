using Insurance.Quote.Application.Handlers
using Insurance.Quote.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Application.UnitTests.Handlers
{
    public class ListQuotesHandlerTests
    {
        private readonly Mock<IQuoteRepository> _quoteRepositoryMock;
        private readonly Mock<ILogger<ListQuotesHandler>> _loggerMock;
        private readonly ListQuotesHandler _handler;

        private readonly CancellationToken CancellationToken = CancellationToken.None;

        public ListQuotesHandlerTests()
        {
            _quoteRepositoryMock = new Mock<IQuoteRepository>();
            _loggerMock = new Mock<ILogger<ListQuotesHandler>>();

            _handler = new ListQuotesHandler(
                _quoteRepositoryMock.Object,
                _loggerMock.Object
            );
        }


        [Fact]
        public async Task HandleAsync_WhenQuotesExist_ShouldReturnListOfQuoteEntities()
        {
            // ARRANGE
            var expectedQuotes = new List<QuoteEntity>
            {
                new QuoteEntity { QuoteId = Guid.NewGuid(), EstimatedValue = 100m },
                new QuoteEntity { QuoteId = Guid.NewGuid(), EstimatedValue = 200m }
            };

            _quoteRepositoryMock
                .Setup(r => r.GetAll(CancellationToken))
                .ReturnsAsync(expectedQuotes);

            // ACT
            var result = await _handler.HandleAsync(CancellationToken);

            // ASSERT
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);

            _quoteRepositoryMock.Verify(r => r.GetAll(CancellationToken), Times.Once);

            Assert.Equal(100m, resultList.First().EstimatedValue);
        }


        [Fact]
        public async Task HandleAsync_WhenNoQuotesExist_ShouldReturnEmptyList()
        {
            // ARRANGE
            _quoteRepositoryMock
                .Setup(r => r.GetAll(CancellationToken))
                .ReturnsAsync(Enumerable.Empty<QuoteEntity>());

            // ACT
            var result = await _handler.HandleAsync(CancellationToken);

            // ASSERT
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task HandleAsync_WhenRepositoryThrowsException_ShouldCatchAndThrowQuoteException()
        {
            // ARRANGE
            _quoteRepositoryMock
                .Setup(r => r.GetAll(CancellationToken))
                .ThrowsAsync(new InvalidOperationException("Erro de conexão simulado."));

            // ACT & ASSERT
            var exception = await Assert.ThrowsAsync<QuoteException>(
                () => _handler.HandleAsync(CancellationToken));

            Assert.Contains("Ocorreu um erro ao tentar listar propostas", exception.Message);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Erro ao alterar listar propostas")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}