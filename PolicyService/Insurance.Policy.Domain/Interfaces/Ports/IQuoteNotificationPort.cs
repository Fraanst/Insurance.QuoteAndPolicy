namespace Insurance.Policy.Domain.Interfaces.Ports
{
    public interface IQuoteNotificationPort
    {
        Task NotifyQuoteApprovedAsync(Guid quoteId, CancellationToken cancellationToken);
    }
}
