namespace Banking.Application.Dto.Customers
{
    public class CustomerIdentityOutputDto
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public bool Active { get; set; }
    }
}
