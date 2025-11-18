

namespace Insurance.Policy.Domain.Entities
{
    public class PolicyEntity
    {
        public Guid Id { get; set; }
        public Guid QuoteId { get; set; }
        public DateTime ContractDate { get; set; }
        public decimal PremiumValue { get; set; }
    }
}
