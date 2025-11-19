namespace Insurance.Policy.Domain.Dto
{
    public class QuoteDto
    {
        public Guid QuoteId { get; set; }
        public string? InsuranceType { get; set; }
        public QuoteStatus Status { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerDto? Customer { get; set; }
        public ProductDto? Product { get; set; }
    }
}
