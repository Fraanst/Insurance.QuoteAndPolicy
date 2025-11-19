using Insurance.Policy.Domain.Dto;

namespace Insurance.Policy.Domain.UnitTests.Dto
{
    public class ProductDtoTests
    {
        private readonly Guid TestProductId = Guid.NewGuid();

        [Fact]
        public void ProductDto_ShouldBeInstantiatedAndPropertiesSetCorrectly()
        {
            // ARRANGE (
            var productType = "Life Standard";
            var value = 50000.55; 

            var dto = new ProductDto
            {
                ProductId = TestProductId,
                ProductType = productType,
                Value = value
            };

            // ASSERT 
            Assert.NotNull(dto);

            Assert.Equal(TestProductId, dto.ProductId);
            Assert.Equal(productType, dto.ProductType);
            Assert.Equal(value, dto.Value);
        }

        [Fact]
        public void ProductDto_ShouldHandleNullAndDefaultValues()
        {
            // ARRANGE & ACT
            var dto = new ProductDto();
            Assert.Equal(Guid.Empty, dto.ProductId);
            Assert.Null(dto.ProductType);           
            Assert.Equal(0.0, dto.Value);         
        }
    }
}