using Insurance.Quote.Domain.Exceptions;

namespace Insurance.Quote.Domain.UnitTests.Exceptions
{
    public class QuoteExceptionTests
    {
        private const string TestMessage = "Erro específico na regra de negócio da Proposta.";


        [Fact]
        public void Constructor_WithMessage_ShouldSetMessageCorrectly()
        {
            // ARRANGE & ACT
            var exception = new QuoteException(TestMessage);

            // ASSERT
            Assert.Equal(TestMessage, exception.Message);
            Assert.Null(exception.InnerException);
            Assert.IsAssignableFrom<DomainException>(exception);
        }


        [Fact]
        public void Constructor_WithMessageAndInnerException_ShouldSetBothCorrectly()
        {
            // ARRANGE
            var innerException = new InvalidOperationException("Erro de IO no banco de dados.");

            // ACT
            var exception = new QuoteException(TestMessage, innerException);

            // ASSERT
            Assert.Equal(TestMessage, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(innerException, exception.InnerException);
        }

        [Fact]
        public void QuoteException_ShouldInheritFromDomainException()
        {
            // ARRANGE & ACT
            var exception = new QuoteException(TestMessage);

            // ASSERT
            Assert.IsAssignableFrom<DomainException>(exception);
        }
    }
}