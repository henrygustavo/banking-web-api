namespace Banking.Application.Dto.Transactions
{
    public class BankTransferInputDto
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }

        public decimal Amount { get; set; }
    }
}
