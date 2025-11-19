using Insurance.Policy.Domain.Dto;

namespace Insurance.Policy.Domain.UnitTests.Dto
{
    public class CustomerDtoTests
    {
        [Fact]
        public void CustomerDto_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE 
            var customerId = Guid.NewGuid();
            var customerName = "João Silva";
            var documentNumber = "123.456.789-00";
            var birthDate = new DateTime(1985, 10, 25).ToShortDateString();

            var dto = new CustomerDto
            {
                CustomerId = customerId,
                CustomerName = customerName,
                DocumentNumber = documentNumber,
                BirthDate = birthDate
            };

            // ASSERT
            Assert.NotNull(dto);

            Assert.Equal(customerId, dto.CustomerId);
            Assert.Equal(customerName, dto.CustomerName);
            Assert.Equal(documentNumber, dto.DocumentNumber);
            Assert.Equal(birthDate, dto.BirthDate);
        }

        [Fact]
        public void CustomerDto_ShouldHandleNullAndDefaultValues()
        {
            var dto = new CustomerDto();

            Assert.Equal(Guid.Empty, dto.CustomerId); 
            Assert.Null(dto.CustomerName);          
            Assert.Null(dto.DocumentNumber);       
            Assert.Equal(default(string), dto.BirthDate);
        }
    }
}