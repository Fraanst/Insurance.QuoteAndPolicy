using Moq;
using Microsoft.Extensions.Logging;
using Insurance.Policy.Application.Handlers;
using Insurance.Policy.Domain.Entities;
using Insurance.Policy.Domain.Exceptions;
using Insurance.Policy.Domain.Interfaces.Repositories;

namespace Insurance.Policy.Application.UnitTests.Handlers
{
    public class GetPolicyByIdHandlerTests
    {
        private readonly Mock<ILogger<ContractQuoteHandler>> _loggerMock;
        private readonly Mock<IPolicyRepository> _policyRepositoryMock;
        private readonly GetPolicyByIdHandler _handler;

        private readonly Guid ExistingPolicyId = Guid.NewGuid();
        private readonly Guid NonExistingPolicyId = Guid.NewGuid();
        private readonly CancellationToken CancellationToken = CancellationToken.None;

        public GetPolicyByIdHandlerTests()
        {
            _loggerMock = new Mock<ILogger<ContractQuoteHandler>>();
            _policyRepositoryMock = new Mock<IPolicyRepository>();

            _handler = new GetPolicyByIdHandler(
                _loggerMock.Object,
                _policyRepositoryMock.Object
            );
        }

        [Fact]
        public async Task HandlerAsync_ExistingId_ShouldReturnPolicyEntity()
        {
            // ARRANGE
            var expectedPolicy = new PolicyEntity { Id = ExistingPolicyId, PremiumValue = 1000m };

            _policyRepositoryMock
                .Setup(r => r.GetPolicyById(ExistingPolicyId, CancellationToken))
                .ReturnsAsync(expectedPolicy);

            // ACT
            var result = await _handler.HandlerAsync(ExistingPolicyId, CancellationToken);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(ExistingPolicyId, result.Id);
            Assert.Equal(1000m, result.PremiumValue);

            _policyRepositoryMock.Verify(r => r.GetPolicyById(ExistingPolicyId, CancellationToken), Times.Once);
        }


        [Fact]
        public async Task HandlerAsync_NonExistingId_ShouldReturnNullPolicy()
        {
            // ARRANGE
            _policyRepositoryMock
                .Setup(r => r.GetPolicyById(NonExistingPolicyId, CancellationToken))
                .ReturnsAsync((PolicyEntity)null!);

            // ACT
            var result = await _handler.HandlerAsync(NonExistingPolicyId, CancellationToken);

            // ASSERT
            Assert.Null(result);

            _policyRepositoryMock.Verify(r => r.GetPolicyById(NonExistingPolicyId, CancellationToken), Times.Once);
        }


        [Fact]
        public async Task HandlerAsync_WhenRepositoryThrowsException_ShouldCatchAndThrowPolicyException()
        {
            // ARRANGE
            _policyRepositoryMock
                .Setup(r => r.GetPolicyById(ExistingPolicyId, CancellationToken))
                .ThrowsAsync(new InvalidOperationException("Erro de conexão simulado."));

            // ACT & ASSERT
            var exception = await Assert.ThrowsAsync<PolicyException>(
                () => _handler.HandlerAsync(ExistingPolicyId, CancellationToken));

            Assert.Contains($"Ocorreu um erro ao tentar buscar Proposta: {ExistingPolicyId}", exception.Message);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Ocorreu um erro ao tentar buscar Proposta: {ExistingPolicyId}")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}