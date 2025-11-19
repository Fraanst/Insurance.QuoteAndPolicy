namespace Quote.Domain.Entities
{
    public class CustomerEntity
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? DocumentNumber { get; set; }
        public string? BirthDate { get; set; }
        public ICollection<QuoteEntity> Quotes { get; set; } = [];

    }
}
