namespace Insurance.Policy.Application.Commands
{
    public class ContractQuoteCommand
    {
        public Guid QuoteId { get; set; }
        public decimal PremiumValue { get; set; }
    }
}
