
namespace Insurance.Quote.Api.Request;

public class CreateQuoteRequest
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public string? InsuranceType { get; set; }
    public QuoteStatus Status { get; set; }
    public decimal EstimatedValue { get; set; }
}
