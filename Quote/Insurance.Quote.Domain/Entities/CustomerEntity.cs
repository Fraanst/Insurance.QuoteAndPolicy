namespace Quote.Domain.Entities
{
    public class CustomerEntity
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
