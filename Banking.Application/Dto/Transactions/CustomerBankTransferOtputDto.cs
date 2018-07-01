namespace Banking.Application.Dto.Transactions
{
    public class CustomerBankTransferOtputDto
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public decimal Balance { get; set; }
    }
}
