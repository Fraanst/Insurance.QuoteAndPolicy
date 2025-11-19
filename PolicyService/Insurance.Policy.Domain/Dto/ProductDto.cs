namespace Insurance.Policy.Domain.Dto
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string? ProductType { get; set; }
        public double Value { get; set; }
    }
}
