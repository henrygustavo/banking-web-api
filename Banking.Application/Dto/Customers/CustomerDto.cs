namespace Banking.Application.Dto.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }
    }
}
