public class PolicyResponse
{
    public Guid Id { get; set; }

    public Guid QuoteId { get; set; }

    public DateTime ContractDate { get; set; }

    public decimal PremiumValue { get; set; }
}