using System.Net;

namespace Insurance.Policy.Domain.Exceptions
{
    public class QuoteNotFoundException : PolicyException
    {
        public QuoteNotFoundException(HttpStatusCode statusCode) : 
            base($"Código: {statusCode}"){}
            
    }
}
