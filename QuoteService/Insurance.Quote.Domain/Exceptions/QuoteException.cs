namespace Insurance.Quote.Domain.Exceptions
{
    public class QuoteException : DomainException
    {
        public QuoteException(string message) : base(message)
        {
        }
        public QuoteException(string message, Exception innerException) : base(message, innerException) { }

    }
}
