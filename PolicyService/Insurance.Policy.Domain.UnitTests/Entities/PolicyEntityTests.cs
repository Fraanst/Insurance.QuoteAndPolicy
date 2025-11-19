using Insurance.Policy.Domain.Entities;

namespace Insurance.Policy.Domain.UnitTests.Entities
{
    public class PolicyEntityTests
    {
        [Fact]
        public void PolicyEntity_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE 
            var policyId = Guid.NewGuid();
            var quoteId = Guid.NewGuid();
            var contractDate = new DateTime(2025, 11, 19, 10, 0, 0, DateTimeKind.Utc);
            var premiumValue = 999.99m;

            // ACT 
            var policy = new PolicyEntity
            {
                Id = policyId,
                QuoteId = quoteId,
                ContractDate = contractDate,
                PremiumValue = premiumValue
            };

            // ASSERT 
            Assert.NotNull(policy);

            Assert.Equal(policyId, policy.Id);
            Assert.Equal(quoteId, policy.QuoteId);
            Assert.Equal(contractDate, policy.ContractDate);
            Assert.Equal(premiumValue, policy.PremiumValue);
        }

        [Fact]
        public void PolicyEntity_ShouldHandleDefaultValues()
        {
            // ARRANGE & ACT
            var policy = new PolicyEntity();

            Assert.Equal(Guid.Empty, policy.Id);          
            Assert.Equal(Guid.Empty, policy.QuoteId);      
            Assert.Equal(default(DateTime), policy.ContractDate); 
            Assert.Equal(0m, policy.PremiumValue);          
        }
    }
}