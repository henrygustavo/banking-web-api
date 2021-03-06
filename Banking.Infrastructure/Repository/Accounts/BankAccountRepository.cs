﻿namespace Banking.Infrastructure.Repository.Accounts
{
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Repository.Accounts;
    using Common;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(BankingContext context)
           : base(context)
        {
        }
        public string GenerateAccountNumber()
        {
            long maxBankAccountNumber = long.Parse(Context.Set<BankAccount>().Max(p => p.Number));

            return maxBankAccountNumber == 0 ? "100000000000000000"  : (maxBankAccountNumber + 1).ToString();
        }

        public IEnumerable<BankAccount> GetAllWithCustomers(int pageNumber, int pageSize,
            string sortBy, string sortDirection)
        {
            var skip = (pageNumber - 1) * pageSize;
            return Context.Set<BankAccount>().Include(p=>p.Customer)
                .OrderBy(sortBy, sortDirection)
                .Skip(skip)
                .Take(pageSize);
        }

        public BankAccount GetWithCustomer(int id)
        {
            return Context.Set<BankAccount>().Where(s => s.Id == id)
                .Include(p => p.Customer)
                .FirstOrDefault();
        }

        public BankAccount GetByNumber(string accountNumber)
        {
            return Context.Set<BankAccount>().FirstOrDefault(s => s.Number == accountNumber);
        }
    }
}
