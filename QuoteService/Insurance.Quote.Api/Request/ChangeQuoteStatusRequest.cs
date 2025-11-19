using Insurance.Quote.Domain.Enums;

namespace Insurance.Quote.Api.Request
{
    public class ChangeQuoteStatusRequest
    {
        public QuoteStatus Status { get; set; }
    }
}
