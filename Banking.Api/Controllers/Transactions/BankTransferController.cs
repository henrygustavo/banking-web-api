namespace Banking.Api.Controllers.Transactions
{
    using System.Collections.Generic;
    using Banking.Application.Service.Transactions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;

    [Produces("application/json")]
    [Route("api/bankTransfer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Member")]
    public class BankTransferController : Controller
    {
        private readonly ITransactionApplicationService _transactionApplicationService;

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