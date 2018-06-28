namespace Banking.Application.Dto.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
