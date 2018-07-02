namespace Banking.Application.Dto.Accounts
{
    public class BankAccountOutputDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerDni { get; set; }
        public bool IsLocked { get; set; }

    }
}
