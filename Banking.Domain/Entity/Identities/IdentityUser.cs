namespace Banking.Domain.Entity.Identities
{
    using Customers;

    public class IdentityUser
    {
        public  int Id { get; set; }
        public  string UserName { get; set; }
        public  string Email { get; set; }

        public  string Role { get; set; }
        public  string Password { get; set; }
        public bool Active { get; set; }
        public Customer Customer { get; set; }

        public bool HasValidCredentials(string userName, string password)
        {
            return UserName == userName && Password == password;
        }
    }
}
