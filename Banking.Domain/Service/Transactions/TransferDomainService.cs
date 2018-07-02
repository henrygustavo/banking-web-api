namespace Banking.Domain.Service.Transactions
{
    using Entity.Accounts;
    using Application.Notification;
    using System;

    public class TransferDomainService : ITransferDomainService
    {
        public void PerformTransfer(BankAccount originAccount, BankAccount destinationAccount, decimal amount)
        {
            Notification notification = Validation(originAccount, destinationAccount, amount);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            originAccount.WithdrawMoney(amount);
            destinationAccount.DepositMoney(amount);
        }

        private Notification Validation(BankAccount originAccount, BankAccount destinationAccount, decimal amount)
        {
            Notification notification = new Notification();
            this.validateAmount(notification, amount);
            this.validateBankAccounts(notification, originAccount, destinationAccount);
            return notification;
        }

        private void validateAmount(Notification notification, decimal amount)
        {
            if (amount <= 0)
            {
                notification.AddError("The amount must be greater than zero");
            }
        }

        private void validateBankAccounts(Notification notification, BankAccount originAccount,
            BankAccount destinationAccount)
        {
            if (originAccount == null || destinationAccount == null)
            {
                notification.AddError("Cannot perform the transfer. Invalid data in bank accounts specifications");
                return;
            }

            if (originAccount.Number == destinationAccount.Number)
            {
                notification.AddError("Cannot transfer money to the same bank account");
            }
        }
    }
}
