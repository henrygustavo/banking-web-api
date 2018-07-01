namespace Banking.Application.Service.Transactions
{
    using Domain.Repository.Common;
    using Banking.Application.Dto.Transactions;
    using System;

    public class TransactionApplicationService: ITransactionApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PerformTransfer(BankTransferInputDto bankTransfertransfer)
        {
            Notification.Common.Notification notification = Validation(bankTransfertransfer);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }
        }

        private Notification.Common.Notification Validation(BankTransferInputDto bankTransfertransfer)
        {
            Notification.Common.Notification notification = new Notification.Common.Notification();

            if (bankTransfertransfer != null) return notification;
            notification.AddError("Invalid JSON data in request body.");
            return notification;
        }
    }
}
