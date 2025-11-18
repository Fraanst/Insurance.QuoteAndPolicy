namespace Insurance.Policy.Domain.Exceptions
{
    public class PolicyException : DomainException
    {
        public PolicyException(string message) : base(message) { }
        public PolicyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
