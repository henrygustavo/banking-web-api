namespace Banking.Api.Controllers.Transactions
{
    using System.Collections.Generic;
    using Banking.Application.Service.Transactions;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/BankTransfer")]
    public class BankTransferController : Controller
    {
        private ITransactionApplicationService _transactionApplicationService;

        public BankTransferController(ITransactionApplicationService transactionApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _transactionApplicationService.PerformTransfer();
            return new string[] { "value1", "value2" };
        }
    }
}