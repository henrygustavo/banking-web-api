namespace Banking.Application.Service.Transactions
{
    using Banking.Application.Dto.Transactions;

    public interface ITransactionApplicationService
    {
       void PerformTransfer(BankTransferInputDto bankTransfer);
    }
}
