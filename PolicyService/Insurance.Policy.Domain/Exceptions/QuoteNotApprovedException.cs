namespace Insurance.Policy.Domain.Exceptions
{
    public class QuoteNotApprovedException : PolicyException
    {
        public QuoteNotApprovedException(Guid quoteId)
            : base($"A Cotação {quoteId} não está aprovada.") { }
    }
}
