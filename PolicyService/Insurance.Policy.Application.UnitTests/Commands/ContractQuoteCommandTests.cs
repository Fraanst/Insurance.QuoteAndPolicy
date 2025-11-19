using Insurance.Policy.Application.Commands;

namespace Insurance.Policy.Application.UnitTests.Commands
{
    public class ContractQuoteCommandTests
    {
        [Fact] 
        public void ContractQuoteCommand_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE 
            var quoteId = Guid.NewGuid();
            var premiumValue = 750.99m;

            // ACT 
            var command = new ContractQuoteCommand
            {
                QuoteId = quoteId,
                PremiumValue = premiumValue
            };

            // ASSERT 
            Assert.NotNull(command);
            Assert.Equal(quoteId, command.QuoteId);
            Assert.Equal(premiumValue, command.PremiumValue);
        }

        [Fact]
        public void ContractQuoteCommand_ShouldHandleDefaultValues()
        {
            // ARRANGE & ACT
            var command = new ContractQuoteCommand();

            // ASSERT
            Assert.Equal(Guid.Empty, command.QuoteId); 
            Assert.Equal(0m, command.PremiumValue);    
        }
    }
}