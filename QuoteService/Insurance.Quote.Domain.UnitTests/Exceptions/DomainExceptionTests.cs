using Insurance.Quote.Domain.Exceptions;

namespace Insurance.Quote.Domain.UnitTests.Exceptions
{
    public class TestableDomainException : DomainException
    {
        public TestableDomainException(string message) : base(message) { }

        public TestableDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class DomainExceptionTests
    {
        private const string TestMessage = "Esta é uma mensagem de erro de domínio.";


        [Fact]
        public void Constructor_WithMessage_ShouldSetMessageCorrectly()
        {
            // ARRANGE & ACT
            var exception = new TestableDomainException(TestMessage);

            // ASSERT
            Assert.Equal(TestMessage, exception.Message);

            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_ShouldSetBothCorrectly()
        {
            // ARRANGE
            var innerException = new InvalidOperationException("Erro de operação interna.");

            // ACT
            var exception = new TestableDomainException(TestMessage, innerException);

            // ASSERT
            Assert.Equal(TestMessage, exception.Message);

            Assert.NotNull(exception.InnerException);
            Assert.Equal(innerException, exception.InnerException);
        }

        [Fact]
        public void TestableDomainException_ShouldInheritFromSystemException()
        {
            // ARRANGE & ACT
            var exception = new TestableDomainException(TestMessage);

            // ASSERT
            Assert.IsAssignableFrom<Exception>(exception);
        }
    }
}