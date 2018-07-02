namespace Banking.Application.Service.Transactions
{
    using Domain.Repository.Common;
    using Banking.Application.Dto.Transactions;
    using System;
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Service.Transactions;
    using Notification;
    public class TransactionApplicationService: ITransactionApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransferDomainService _transferDomainService;
        public TransactionApplicationService(IUnitOfWork unitOfWork, ITransferDomainService transferDomainService)
        {
            _unitOfWork = unitOfWork;
            _transferDomainService = transferDomainService;
        }

        public void PerformTransfer(BankTransferInputDto bankTransfer)
        {
            Notification notification = Validation(bankTransfer);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            BankAccount originAccount = _unitOfWork.BankAccounts.GetByNumber(bankTransfer.FromAccountNumber);
            BankAccount destinationAccount = _unitOfWork.BankAccounts.GetByNumber(bankTransfer.ToAccountNumber);

            _transferDomainService.PerformTransfer(originAccount,destinationAccount, bankTransfer.Amount);
            _unitOfWork.BankAccounts.Update(originAccount);
            _unitOfWork.BankAccounts.Update(destinationAccount);

            _unitOfWork.Complete();
        }

        private Notification Validation(BankTransferInputDto bankTransfertransfer)
        {
            Notification notification = new Notification();

            if (bankTransfertransfer != null) return notification;
            notification.AddError("Invalid JSON data in request body.");
            return notification;
        }
    }
}
