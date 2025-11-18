using Quote.Domain.Entities;

namespace Insurance.Quote.Api.Response
{
    public class QuoteResponse
    {
        public Guid QuoteId { get; set; }
        public string? InsuranceType { get; set; }
        public QuoteStatus Status { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerEntity? Customer { get; set; }
        public ProductEntity? Product { get; set; }
    }

}
