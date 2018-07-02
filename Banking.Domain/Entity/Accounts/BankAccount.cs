namespace Banking.Domain.Entity.Accounts
{
    using Customers;
    using Application.Notification;
    using System;

    public class BankAccount
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public bool IsLocked { get; set; }
        public Customer Customer { get; set; }

        public bool HasIdentity()
        {
            return Number.Trim() != string.Empty;
        }

        public void WithdrawMoney(decimal amount)
        {
            Notification notification = WithdrawValidation(amount);
            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }
            Balance = Balance - amount;
        }

        public void DepositMoney(decimal amount)
        {
            Notification notification = DepositValidation(amount);
            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }
            Balance = Balance + amount;
        }

        public Notification WithdrawValidation(decimal amount)
        {
            Notification notification = new Notification();
            validateAmount(notification, amount);
            ValidateBankAccount(notification);
            ValidateBalance(notification, amount);
            return notification;
        }

        public Notification DepositValidation(decimal amount)
        {
            Notification notification = new Notification();
            validateAmount(notification, amount);
            ValidateBankAccount(notification);
            return notification;
        }

        private void validateAmount(Notification notification, decimal amount)
        {
            if (amount <= 0)
            {
                notification.AddError("The amount must be greater than zero");
            }
        }

        private void ValidateBankAccount(Notification notification)
        {
            if (!HasIdentity())
            {
                notification.AddError("The account has no identity");
            }
            if (IsLocked)
            {
                notification.AddError("The account is locked");
            }
        }

        private void ValidateBalance(Notification notification, decimal amount)
        {
            if (!CanBeWithdrawed(amount))
            {
                notification.AddError("Cannot withdraw in the account, amount is greater than Balance");
            }
        }

        public bool CanBeWithdrawed(decimal amount)
        {
            return !IsLocked && Balance >= amount;
        }
    }
}
