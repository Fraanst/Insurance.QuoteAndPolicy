using Xunit;
using System;
using System.Linq;
using Quote.Domain.Entities;

namespace Insurance.Quote.Domain.UnitTests.Entities
{
    public class ProductEntityTests
    {
        [Fact]
        public void ProductEntity_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE 
            var productId = Guid.NewGuid();
            var productType = "Health Gold";
            var value = 50000.75m;

            // ACT 
            var product = new ProductEntity
            {
                ProductId = productId,
                ProductType = productType,
                Value = value
            };

            // ASSERT
            Assert.NotNull(product);
            Assert.Equal(productId, product.ProductId);
            Assert.Equal(productType, product.ProductType);
            Assert.Equal(value, product.Value);

            Assert.NotNull(product.Quotes);
            Assert.Empty(product.Quotes);
        }

        [Fact]
        public void ProductEntity_QuotesCollection_ShouldAllowAddingQuotes()
        {
            // ARRANGE
            var product = new ProductEntity();
            var quote1 = new QuoteEntity { QuoteId = Guid.NewGuid(), EstimatedValue = 100m };
            var quote2 = new QuoteEntity { QuoteId = Guid.NewGuid(), EstimatedValue = 200m };

            // ACT
            product.Quotes.Add(quote1);
            product.Quotes.Add(quote2);

            // ASSERT
            Assert.Equal(2, product.Quotes.Count);

            Assert.Contains(product.Quotes, q => q.QuoteId == quote1.QuoteId);
            Assert.Contains(product.Quotes, q => q.EstimatedValue == 200m);
        }

        [Fact]
        public void ProductEntity_ShouldHandleNullProductTypeAndDefaultValues()
        {
            // ARRANGE & ACT: 
            var product = new ProductEntity
            {
                ProductType = null,
                Value = default 
            };

            // ASSERT
            Assert.Null(product.ProductType);
            Assert.Equal(Guid.Empty, product.ProductId);
            Assert.Equal(0m, product.Value);
        }
    }
}