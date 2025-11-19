
using Insurance.Quote.Domain.Enums;

namespace Insurance.Quote.Api.Request;

public class CreateQuoteRequest
{
    public string? InsuranceType { get; set; }
    public QuoteStatus Status { get; set; }
    public decimal EstimatedValue { get; set; }
}
