namespace Insurance.Policy.Domain.Exceptions
{
    public class QuoteNotApprovedException : PolicyException
    {
        public QuoteNotApprovedException(Guid quoteId)
            : base($"A proposta com ID {quoteId} não está aprovada.") { }
    }
}
