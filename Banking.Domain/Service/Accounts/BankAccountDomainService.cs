namespace Banking.Domain.Service.Accounts
{
    using System;
    using Application.Notification;
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Entity.Customers;

    public class BankAccountDomainService: IBankAccountDomainService
    {
        public void PerformNewBankAccount(Customer customer,
                                        BankAccount bankAccountWithSameNumber,
                                        string accountNumber, bool isLocked)
        {
            Notification notification = ValidationInsert(customer, bankAccountWithSameNumber);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            customer.BankAccounts.Add(new BankAccount
            {
                Number = accountNumber,
                Balance = 1000, //just for demo
                IsLocked = isLocked
            });
        }

        public void PerformUpdateBankAccount(BankAccount bankAccount, bool isLocked)
        {
            Notification notification = ValidationUpdate(bankAccount);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            bankAccount.IsLocked = isLocked;
        }

        private Notification ValidationUpdate(BankAccount bankAccount)
        {
            Notification notification = new Notification();
            this.ValidateBankAccount(notification, bankAccount);
            return notification;
        }

        private Notification ValidationInsert(Customer customer, BankAccount bankAccountWithSameNumber)
        {
            Notification notification = new Notification();
            this.ValidateCustomer(notification, customer);
            this.ValidateDuplicatedAccountNumber(notification, bankAccountWithSameNumber);

            return notification;
        }

        private void ValidateCustomer(Notification notification, Customer customer)
        {
            if (customer == null)
            {
                notification.AddError("Customer does not exists.");
            }
        }

        private void ValidateDuplicatedAccountNumber(Notification notification, BankAccount bankAccountWithSameNumber)
        {
            if (bankAccountWithSameNumber != null)
            {
                notification.AddError("Bank Account number already exists.");
            }
        }

        private void ValidateBankAccount(Notification notification, BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                notification.AddError("Bank Account number doesn't exist.");
            }
        }
    }
}
