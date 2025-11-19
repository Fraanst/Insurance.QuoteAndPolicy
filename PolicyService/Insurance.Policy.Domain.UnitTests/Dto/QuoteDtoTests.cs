using Insurance.Policy.Domain.Dto;

namespace Insurance.Policy.Domain.UnitTests.Dto
{
    public class QuoteDtoTests
    {
        private readonly Guid TestQuoteId = Guid.NewGuid();
        private readonly Guid TestCustomerId = Guid.NewGuid();
        private readonly Guid TestProductId = Guid.NewGuid();

        [Fact]
        public void QuoteDto_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE
            var now = DateTime.UtcNow;
            var estimatedValue = 2500.99m;

            var customerDto = new CustomerDto { CustomerId = TestCustomerId };
            var productDto = new ProductDto { ProductId = TestProductId };

            // ACT
            var dto = new QuoteDto
            {
                QuoteId = TestQuoteId,
                InsuranceType = "Auto Completo",
                Status = QuoteStatus.Approved,
                EstimatedValue = estimatedValue,
                CreatedAt = now,
                Customer = customerDto,
                Product = productDto
            };

            // ASSERT
            Assert.NotNull(dto);

            Assert.Equal(TestQuoteId, dto.QuoteId);
            Assert.Equal("Auto Completo", dto.InsuranceType);
            Assert.Equal(QuoteStatus.Approved, dto.Status);
            Assert.Equal(estimatedValue, dto.EstimatedValue);
            Assert.Equal(now, dto.CreatedAt);

            Assert.NotNull(dto.Customer);
            Assert.Equal(TestCustomerId, dto.Customer.CustomerId);
            Assert.NotNull(dto.Product);
            Assert.Equal(TestProductId, dto.Product.ProductId);
        }

        [Fact]
        public void QuoteDto_ShouldHandleNullAndDefaultValues()
        {
            // ARRANGE & ACT
            var dto = new QuoteDto();

            // ASSERT
            Assert.Null(dto.InsuranceType);
            Assert.Null(dto.Customer);
            Assert.Null(dto.Product);

            Assert.Equal(Guid.Empty, dto.QuoteId);
            Assert.Equal(default(QuoteStatus), dto.Status); 
            Assert.Equal(0m, dto.EstimatedValue);
            Assert.Equal(default(DateTime), dto.CreatedAt);
        }
    }
}