namespace Insurance.Quote.Domain.Exceptions
{
    public class QuoteStatusChangeFailedException : QuoteException
    {
        public QuoteStatusChangeFailedException(Guid Id, string status)
           : base($"Não é possível alterar o status da proposta de {Id} para {status}.") {}
    }

}