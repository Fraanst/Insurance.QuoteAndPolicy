namespace Quote.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }
        public string? ProductType { get; set; }
        public decimal Value { get; set; }

        public ICollection<QuoteEntity> Quotes { get; set; } = new List<QuoteEntity>();
    }
}
