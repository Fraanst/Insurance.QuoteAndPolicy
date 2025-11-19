using Insurance.Policy.Domain.Exceptions;

namespace Insurance.Policy.Domain.UnitTests.Exceptions
{
    public class PolicyExceptionTests
    {
        private const string TestMessage = "Erro específico na regra de negócio do Contrato.";


        [Fact]
        public void Constructor_WithMessage_ShouldSetMessageCorrectly()
        {
            // ARRANGE & ACT
            var exception = new PolicyException(TestMessage);

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
            var exception = new PolicyException(TestMessage, innerException);

            // ASSERT
            Assert.Equal(TestMessage, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(innerException, exception.InnerException);
        }

        [Fact]
        public void PolicyException_ShouldInheritFromDomainException()
        {
            // ARRANGE & ACT
            var exception = new PolicyException(TestMessage);

            // ASSERT
            Assert.IsAssignableFrom<DomainException>(exception);
        }
    }
}