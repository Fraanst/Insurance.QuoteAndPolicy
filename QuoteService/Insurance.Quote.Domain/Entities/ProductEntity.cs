namespace Quote.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }
        public string? ProductType { get; set; }
        public double Value { get; set; }
    }
}
