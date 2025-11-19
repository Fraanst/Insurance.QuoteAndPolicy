using Insurance.Quote.Domain.Enums;

namespace Insurance.Quote.Application.Commands
{
    public class CreateQuoteCommand
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string? InsuranceType { get; set; }
        public QuoteStatus Status { get; set; }
        public decimal EstimatedValue { get; set; }
    }
}
