using Xunit;
using System;
using Quote.Domain.Entities;

namespace Insurance.Quote.Domain.UnitTests.Entities
{
    public class QuoteEntityTests
    {

        [Fact]
        public void QuoteEntity_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE
            var quoteId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var estimatedValue = 1234.56m;

            // ACT
            var quote = new QuoteEntity
            {
                QuoteId = quoteId,
                CustomerId = customerId,
                ProductId = productId,
                InsuranceType = "Residencial",
                Status = QuoteStatus.UnderReview,
                EstimatedValue = estimatedValue,
                CreatedAt = now,
                Customer = new CustomerEntity { CustomerId = customerId },
                Product = new ProductEntity { ProductId = productId }
            };

            // ASSERT
            Assert.NotNull(quote);
            Assert.Equal(quoteId, quote.QuoteId);
            Assert.Equal(QuoteStatus.UnderReview, quote.Status);
            Assert.Equal(estimatedValue, quote.EstimatedValue);
            Assert.Equal(now, quote.CreatedAt);
            Assert.NotNull(quote.Customer);
            Assert.NotNull(quote.Product);
        }


        [Theory]
        [InlineData(QuoteStatus.UnderReview, QuoteStatus.Approved, true)] 
        [InlineData(QuoteStatus.UnderReview, QuoteStatus.Rejected, true)]
        [InlineData(QuoteStatus.Approved, QuoteStatus.Rejected, false)] 
        public void CanChangeStatusTo_ShouldReturnCorrectlyBasedOnCurrentStatus(
            QuoteStatus currentStatus, QuoteStatus targetStatus, bool expectedResult)
        {
            // ARRANGE
            var quote = new QuoteEntity { Status = currentStatus };

            // ACT
            var canChange = quote.CanChangeStatusTo(targetStatus);

            // ASSERT
            Assert.Equal(expectedResult, canChange);
        }


        [Fact]
        public void ChangeStatus_ShouldUpdateStatusWhenCalled()
        {
            // ARRANGE
            var quote = new QuoteEntity { Status = QuoteStatus.UnderReview };
            var newStatus = QuoteStatus.Approved;

            // ACT
            quote.ChangeStatus(newStatus);

            // ASSERT
            Assert.Equal(newStatus, quote.Status);
        }

        [Fact]
        public void ChangeStatus_CanChangeToSameStatus()
        {
            // ARRANGE
            var currentStatus = QuoteStatus.Approved;
            var quote = new QuoteEntity { Status = currentStatus };

            // ACT
            quote.ChangeStatus(currentStatus);

            // ASSERT
            Assert.Equal(currentStatus, quote.Status);
        }
    }
}