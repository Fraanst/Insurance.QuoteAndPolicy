
using Insurance.Quote.Domain.Exceptions;
using Insurance.Quote.Domain.Interfaces.Ports;
using Microsoft.Extensions.Logging;
using Moq;
using Quote.Domain.Entities;
using Quote.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class ChangeQuoteStatusHandlerTests
{
    private readonly Mock<IQuoteRepository> _quoteRepositoryMock;
    private readonly Mock<ILogger<ChangeQuoteStatusHandler>> _loggerMock;
    private readonly Mock<IQuoteNotificationPort> _notificationPortMock;
    private readonly ChangeQuoteStatusHandler _handler;

    private readonly Guid QuoteId = Guid.NewGuid();
    private readonly CancellationToken CancellationToken = CancellationToken.None;

    public ChangeQuoteStatusHandlerTests()
    {
        _quoteRepositoryMock = new Mock<IQuoteRepository>();
        _loggerMock = new Mock<ILogger<ChangeQuoteStatusHandler>>();
        _notificationPortMock = new Mock<IQuoteNotificationPort>();

        _handler = new ChangeQuoteStatusHandler(
            _quoteRepositoryMock.Object,
            _loggerMock.Object,
            _notificationPortMock.Object
        );
    }

    [Fact]
    public async Task HandleAsync_WhenStatusChangesToApproved_ShouldUpdateAndNotify()
    {
        var initialStatus = QuoteStatus.UnderReview;
        var newStatus = QuoteStatus.Approved;

        var mockQuote = new QuoteEntity
        {
            Status = initialStatus,
            QuoteId = QuoteId
        };

        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(QuoteId, CancellationToken))
            .ReturnsAsync(mockQuote);

        // ACT
        await _handler.HandleAsync(QuoteId, newStatus, CancellationToken);

        // ASSERT
        _quoteRepositoryMock.Verify(
            r => r.UpdateStatusAsync(mockQuote, CancellationToken),
            Times.Once);

        _notificationPortMock.Verify(
            n => n.NotifyQuoteApprovedAsync(QuoteId, CancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenStatusChangesIsRejected_ShouldUpdateButNotNotify()
    {
        // ARRANGE
        var newStatus = QuoteStatus.Rejected;

        var mockQuote = new QuoteEntity
        {
            Status = QuoteStatus.UnderReview,
            QuoteId = QuoteId
        };

        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(QuoteId, CancellationToken))
            .ReturnsAsync(mockQuote);

        // ACT
        await _handler.HandleAsync(QuoteId, newStatus, CancellationToken);

        // ASSERT
        _quoteRepositoryMock.Verify(r => r.UpdateStatusAsync(It.IsAny<QuoteEntity>(), CancellationToken), Times.Once);

        _notificationPortMock.Verify(
            n => n.NotifyQuoteApprovedAsync(It.IsAny<Guid>(), CancellationToken),
            Times.Never);
    }


    [Fact]
    public async Task HandleAsync_WhenQuoteNotFound_ShouldThrowKeyNotFoundException()
    {
        // ARRANGE
        var newStatus = QuoteStatus.Approved;

        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(QuoteId, CancellationToken))
            .ReturnsAsync((QuoteEntity)null!); 

        // ACT & ASSERT
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.HandleAsync(QuoteId, newStatus, CancellationToken));
    }

    [Fact]
    public async Task HandleAsync_WhenCanChangeStatusToIsFalse_ShouldThrowQuoteStatusChangeFailedException()
    {
        // ARRANGE
        var newStatus = QuoteStatus.Approved;
        var mockQuote = new QuoteEntity
        {
            Status = QuoteStatus.Rejected, 
            QuoteId = QuoteId
        };

        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(QuoteId, CancellationToken))
            .ReturnsAsync(mockQuote);

        // ACT & ASSERT
        await Assert.ThrowsAsync<QuoteStatusChangeFailedException>(
            () => _handler.HandleAsync(QuoteId, newStatus, CancellationToken));

        _quoteRepositoryMock.Verify(
            r => r.UpdateStatusAsync(It.IsAny<QuoteEntity>(), CancellationToken),
            Times.Never);
    }

    [Fact]
    public async Task HandleAsync_WhenRepositoryThrowsException_ShouldThrowQuoteException()
    {
        // ARRANGE
        var newStatus = QuoteStatus.Approved;

        _quoteRepositoryMock
            .Setup(r => r.GetByIdAsync(QuoteId, CancellationToken))
            .ThrowsAsync(new InvalidOperationException("Erro de conexão com o DB."));

        // ACT & ASSERT
        await Assert.ThrowsAsync<QuoteException>(
            () => _handler.HandleAsync(QuoteId, newStatus, CancellationToken));

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Erro ao alterar status da proposta")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);
    }
}