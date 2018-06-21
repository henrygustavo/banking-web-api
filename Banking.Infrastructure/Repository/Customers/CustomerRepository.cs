namespace Banking.Infrastructure.Repository.Customers
{
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Repository.Customers;
    using Common;

    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankingContext context)
           : base(context)
        {
        }

    }
}
