namespace Insurance.Quote.Domain.Exceptions
{
    public class QuoteNotFoundException : QuoteException
    {
        public QuoteNotFoundException() :
            base("Proposta não encontrada"){ }
    }
}
