namespace Insurance.Policy.Domain.Dto
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? DocumentNumber { get; set; }
        public string? BirthDate { get; set; }
    }
}
