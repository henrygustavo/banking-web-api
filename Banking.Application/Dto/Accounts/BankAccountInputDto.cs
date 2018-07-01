namespace Banking.Application.Dto.Accounts
{
    public class BankAccountInputDto
    {
       public int Id { get; set; }     
       public  string Number { get; set; }
       public int CustomerId { get; set; }
       public  bool IsLocked { get; set; }

    }
}
