using Insurance.Policy.Domain.Exceptions;

namespace Insurance.Policy.Domain.UnitTests.Exceptions
{
    public class QuoteNotApprovedExceptionTests
    {
        private readonly Guid TestQuoteId = Guid.NewGuid();

        [Fact]
        public void Constructor_ShouldFormatMessageCorrectly()
        {
            // ARRANGE
            var expectedMessage = $"A Cotação {TestQuoteId} não está aprovada.";
            // ACT
            var exception = new QuoteNotApprovedException(TestQuoteId);

            // ASSERT
            Assert.Equal(expectedMessage, exception.Message);

        }

        [Fact]
        public void QuoteNotApprovedException_ShouldInheritFromPolicyException()
        {
            // ARRANGE & ACT
            var exception = new QuoteNotApprovedException(TestQuoteId);

            // ASSERT
            Assert.IsAssignableFrom<PolicyException>(exception);
        }
    }
}