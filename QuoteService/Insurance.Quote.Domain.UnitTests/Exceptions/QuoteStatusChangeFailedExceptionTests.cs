using Insurance.Quote.Domain.Exceptions;

namespace Insurance.Quote.Domain.UnitTests.Exceptions
{
    public class QuoteStatusChangeFailedExceptionTests
    {
        private readonly Guid TestId = Guid.NewGuid();
        private const string TestStatus = "Approved";

        [Fact]
        public void Constructor_WithMessage_ShouldFormatMessageCorrectly()
        {
            // ARRANGE
            var expectedMessage = $"Não é possível alterar o status da proposta de {TestId} para {TestStatus}.";

            // ACT
            var exception = new QuoteStatusChangeFailedException(TestId, TestStatus);

            // ASSERT
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void QuoteStatusChangeFailedException_ShouldInheritFromQuoteException()
        {
            // ARRANGE & ACT
            var exception = new QuoteStatusChangeFailedException(TestId, TestStatus);

            // ASSERT
            Assert.IsAssignableFrom<QuoteException>(exception);
        }
    }
}