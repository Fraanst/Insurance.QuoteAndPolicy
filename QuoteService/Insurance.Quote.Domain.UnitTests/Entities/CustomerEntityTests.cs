using Quote.Domain.Entities;

namespace Insurance.Quote.Domain.UnitTests.Entities
{
    public class CustomerEntityTests
    {
        [Fact]
        public void CustomerEntity_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE 
            var customerId = Guid.NewGuid();
            var name = "Alice Johnson";
            var document = "123.456.789-00";
            var birthDate = new DateTime(1990, 5, 15);

            // ACT 
            var customer = new CustomerEntity
            {
                CustomerId = customerId,
                CustomerName = name,
                DocumentNumber = document,
                BirthDate = birthDate
            };

            // ASSERT
            Assert.NotNull(customer);
            Assert.Equal(customerId, customer.CustomerId);
            Assert.Equal(name, customer.CustomerName);
            Assert.Equal(document, customer.DocumentNumber);
            Assert.Equal(birthDate, customer.BirthDate);

            Assert.NotNull(customer.Quotes);
            Assert.Empty(customer.Quotes);
        }

        [Fact]
        public void CustomerEntity_QuotesCollection_ShouldAllowAddingQuotes()
        {
            // ARRANGE
            var customer = new CustomerEntity();
            var quote1 = new QuoteEntity { QuoteId = Guid.NewGuid(), EstimatedValue = 1000m };
            var quote2 = new QuoteEntity { QuoteId = Guid.NewGuid(), EstimatedValue = 2500m };

            // ACT
            customer.Quotes.Add(quote1);
            customer.Quotes.Add(quote2);

            // ASSERT
            Assert.Equal(2, customer.Quotes.Count);

            Assert.Contains(customer.Quotes, q => q.QuoteId == quote1.QuoteId);
            Assert.Contains(customer.Quotes, q => q.EstimatedValue == 2500m);
        }

        [Fact]
        public void CustomerEntity_ShouldHandleNullCustomerNameAndDocumentNumber()
        {
            var customer = new CustomerEntity
            {
                CustomerName = null,
                DocumentNumber = null,
                BirthDate = default 
            };

            // ASSERT
            Assert.Null(customer.CustomerName);
            Assert.Null(customer.DocumentNumber);

            Assert.Equal(Guid.Empty, customer.CustomerId);
            Assert.Equal(default(DateTime), customer.BirthDate);
        }
    }
}