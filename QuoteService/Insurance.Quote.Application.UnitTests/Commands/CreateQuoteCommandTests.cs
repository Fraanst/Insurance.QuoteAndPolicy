using Insurance.Quote.Application.Commands;
using Insurance.Quote.Domain.Enums;

namespace Insurance.Quote.Application.UnitTests.Commands
{
    public class CreateQuoteCommandTests
    {
        [Fact] 
        public void CreateQuoteCommand_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var insuranceType = "Auto";
            var status = QuoteStatus.UnderReview;
            var estimatedValue = 150.50m;

            var command = new CreateQuoteCommand
            {
                CustomerId = customerId,
                ProductId = productId,
                InsuranceType = insuranceType,
                Status = status,
                EstimatedValue = estimatedValue
            };

            Assert.NotNull(command);

            Assert.Equal(customerId, command.CustomerId);
            Assert.Equal(productId, command.ProductId);
            Assert.Equal(insuranceType, command.InsuranceType);
            Assert.Equal(status, command.Status);
            Assert.Equal(estimatedValue, command.EstimatedValue);
        }

        [Fact]
        public void CreateQuoteCommand_ShouldHandleNullAndDefaultValues()
        {
            var command = new CreateQuoteCommand();

            Assert.Equal(Guid.Empty, command.CustomerId);
            Assert.Equal(Guid.Empty, command.ProductId);
            Assert.Null(command.InsuranceType);
            Assert.Equal(default(QuoteStatus), command.Status); 
            Assert.Equal(0m, command.EstimatedValue);
        }
    }
}