using Insurance.Policy.Application.Commands;
using Insurance.Policy.Application.Handlers;
using Insurance.Policy.Domain.Dto;
using Insurance.Policy.Domain.Entities;
using Insurance.Policy.Domain.Exceptions;
using Insurance.Policy.Domain.Interfaces.Ports;
using Insurance.Policy.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Insurance.Policy.Application.UnitTests.Handlers
{
    public class ContractQuoteHandlerTests
    {
        private readonly Mock<ILogger<ContractQuoteHandler>> _loggerMock;
        private readonly Mock<IQuoteServicePort> _quoteServicePortMock;
        private readonly Mock<IPolicyRepository> _policyRepositoryMock;
        private readonly ContractQuoteHandler _handler;

        private readonly Guid TestQuoteId = Guid.NewGuid();
        private readonly decimal TestPremiumValue = 500.50m;
        private readonly CancellationToken CancellationToken = CancellationToken.None;

        public ContractQuoteHandlerTests()
        {
            _loggerMock = new Mock<ILogger<ContractQuoteHandler>>();
            _quoteServicePortMock = new Mock<IQuoteServicePort>();
            _policyRepositoryMock = new Mock<IPolicyRepository>();


            _handler = new ContractQuoteHandler(
                _loggerMock.Object,
                _quoteServicePortMock.Object,
                _policyRepositoryMock.Object
            );
        }


        [Fact]
        public async Task HandleAsync_WhenQuoteIsApproved_ShouldCreatePolicyAndReturnIt()
        {
            // ARRANGE
            var command = new ContractQuoteCommand
            {
                QuoteId = TestQuoteId,
                PremiumValue = TestPremiumValue
            };

            var approvedQuote = new QuoteDto { QuoteId = TestQuoteId, Status = QuoteStatus.Approved };
            _quoteServicePortMock
                .Setup(p => p.GetQuoteAsync(TestQuoteId, CancellationToken))
                .ReturnsAsync(approvedQuote);

            // ACT
            var resultPolicy = await _handler.HandleAsync(command, CancellationToken);

            // ASSERT
            Assert.NotNull(resultPolicy);
            Assert.Equal(TestQuoteId, resultPolicy.QuoteId);
            Assert.Equal(TestPremiumValue, resultPolicy.PremiumValue);

            _policyRepositoryMock.Verify(
                r => r.ContractQuote(
                    It.Is<PolicyEntity>(p =>
                        p.QuoteId == TestQuoteId &&
                        p.PremiumValue == TestPremiumValue),
                    CancellationToken),
                Times.Once);
        }

        [Theory]
        [InlineData(QuoteStatus.UnderReview)]
        [InlineData(QuoteStatus.Rejected)]
        public async Task HandleAsync_WhenQuoteIsNotApproved_ShouldThrowQuoteNotApprovedException(QuoteStatus nonApprovedStatus)
        {
            // ARRANGE
            var command = new ContractQuoteCommand
            {
                QuoteId = TestQuoteId,
                PremiumValue = TestPremiumValue
            };

            var unapprovedQuote = new QuoteDto { QuoteId = TestQuoteId, Status = nonApprovedStatus };
            _quoteServicePortMock
                .Setup(p => p.GetQuoteAsync(TestQuoteId, CancellationToken))
                .ReturnsAsync(unapprovedQuote);

            // ACT & ASSERT
            await Assert.ThrowsAsync<QuoteNotApprovedException>(
                () => _handler.HandleAsync(command, CancellationToken));

            _policyRepositoryMock.Verify(
                r => r.ContractQuote(It.IsAny<PolicyEntity>(), CancellationToken),
                Times.Never);
        }


        [Fact]
        public async Task HandleAsync_WhenQuoteServiceThrowsException_ShouldThrowPolicyException()
        {
            // ARRANGE
            var command = new ContractQuoteCommand
            {
                QuoteId = TestQuoteId,
                PremiumValue = TestPremiumValue
            };

            _quoteServicePortMock
                .Setup(p => p.GetQuoteAsync(TestQuoteId, CancellationToken))
                .ThrowsAsync(new TimeoutException("Serviço de Quote fora do ar."));

            // ACT & ASSERT
            await Assert.ThrowsAsync<PolicyException>(
                () => _handler.HandleAsync(command, CancellationToken));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Ocorreu um erro ao tentar contratar proposta")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}