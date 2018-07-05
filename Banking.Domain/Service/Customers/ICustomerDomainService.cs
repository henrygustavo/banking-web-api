namespace Banking.Domain.Service.Customers
{
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Entity.Identities;

    public interface ICustomerDomainService
    {
        void PerformNewCustomer(Customer customer, Customer customerWithSameDni, int identityUserId);

        void PerformUpdateCustomer(Customer customer, string firstName, string lastName, bool active,
                                  IdentityUser identityUser);
    }
}
