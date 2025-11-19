using Moq;
using Microsoft.Extensions.Logging;
using Quote.Domain.Interfaces.Repositories;
using Insurance.Quote.Domain.Exceptions;
using Quote.Domain.Entities;

public class GetQuoteByIdHandlerTests
{
    private readonly Mock<IQuoteRepository> _quoteRepositoryMock;
    private readonly Mock<ILogger<GetQuoteByIdHandler>> _loggerMock;
    private readonly GetQuoteByIdHandler _handler;

    private readonly Guid ExistingQuoteId = Guid.NewGuid();
    private readonly Guid NonExistingQuoteId = Guid.NewGuid();
    private readonly CancellationToken CancellationToken = CancellationToken.None;

    public GetQuoteByIdHandlerTests()
    {
        _quoteRepositoryMock = new Mock<IQuoteRepository>();
        _loggerMock = new Mock<ILogger<GetQuoteByIdHandler>>();

        _handler = new GetQuoteByIdHandler(
            _quoteRepositoryMock.Object,
            _loggerMock.Object
        );
    }


    [Fact]
    public async Task HandleAsync_ExistingId_ShouldReturnQuoteEntity()
    {
        // ARRANGE
        var expectedQuote = new QuoteEntity { QuoteId = ExistingQuoteId };

        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(ExistingQuoteId, CancellationToken))
            .ReturnsAsync(expectedQuote);

        // ACT
        var result = await _handler.HandleAsync(ExistingQuoteId, CancellationToken);

        // ASSERT

        Assert.NotNull(result);
        Assert.Equal(ExistingQuoteId, result.QuoteId);

        _quoteRepositoryMock.Verify(r => r.GetByIdAsync(ExistingQuoteId, CancellationToken), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_NonExistingId_ShouldReturnNull()
    {
        // ARRANGE
        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(NonExistingQuoteId, CancellationToken))
            .ReturnsAsync((QuoteEntity)null!);

        // ACT
        var result = await _handler.HandleAsync(NonExistingQuoteId, CancellationToken);

        // ASSERT
        Assert.Null(result);

        _quoteRepositoryMock.Verify(r => r.GetByIdAsync(NonExistingQuoteId, CancellationToken), Times.Once);
    }


    [Fact]
    public async Task HandleAsync_WhenRepositoryThrowsException_ShouldCatchAndThrowQuoteException()
    {
        // ARRANGE
        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(ExistingQuoteId, CancellationToken))
            .ThrowsAsync(new InvalidOperationException("Erro de conexão simulado."));

        // ACT & ASSERT
        var exception = await Assert.ThrowsAsync<QuoteException>(
            () => _handler.HandleAsync(ExistingQuoteId, CancellationToken));

        Assert.Contains("Ocorreu um erro ao tentar buscar uma proposta", exception.Message);

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Erro ao alterar buscar uma proposta")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);
    }
}