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
        var exception = await Assert.ThrowsAsync<QuoteNotFoundException>(
            () => _handler.HandleAsync(NonExistingQuoteId, CancellationToken));

        Assert.Contains("Proposta não encontrada", exception.Message);

        // ASSERT
        _quoteRepositoryMock.Verify(r => r.GetByIdAsync(NonExistingQuoteId, CancellationToken), Times.Once);
    }
}