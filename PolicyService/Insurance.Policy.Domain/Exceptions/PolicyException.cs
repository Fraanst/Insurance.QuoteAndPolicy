
namespace Insurance.Policy.Domain.Exceptions
{
    public class PolicyException : DomainException
    {
        public int StatusCode { get; }

        public PolicyException(string message, int statusCode = 500)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public PolicyException(string message, Exception innerException, int statusCode = 500) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
