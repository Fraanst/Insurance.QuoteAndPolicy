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
    }
}
