﻿namespace Banking.Domain.Repository.Customers
{
    using Banking.Domain.Entity.Customers;
    using Common;

    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByIdWithBankAccounts(int id);
        Customer GetByDni(string dni);
    }
}
