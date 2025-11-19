using Insurance.Quote.Domain.Enums;

namespace Quote.Domain.Entities
{
    public class QuoteEntity
    {
        public Guid QuoteId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string? InsuranceType { get; set; }
        public QuoteStatus Status { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerEntity? Customer { get; set; }
        public ProductEntity? Product { get; set; }


        public bool CanChangeStatusTo(QuoteStatus status)
        {
            if(Status != QuoteStatus.UnderReview)
                return false;
            return true;
        }

        public void ChangeStatus(QuoteStatus newStatus)
            => Status = newStatus;
    }
}
