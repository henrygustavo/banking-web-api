namespace Banking.Infrastructure.Repository.Customers
{
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Repository.Customers;
    using Common;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankingContext context)
           : base(context)
        {
           
        }

        public Customer GetByIdWithBankAccounts(int id)
        {
            return Context.Set<Customer>().Where(s => s.Id == id)
                                          .Include(p => p.BankAccounts)
                                          .FirstOrDefault();
        }

        public Customer GetByDni(string dni)
        {
            return Context.Set<Customer>().FirstOrDefault(s => s.Dni == dni);
        }
    }
}
