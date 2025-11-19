namespace Insurance.Quote.Domain.Interfaces.Ports
{
    public interface IQuoteNotificationPort
    {
        Task NotifyQuoteApprovedAsync(Guid quoteId, CancellationToken cancellationToken);
    }
}
