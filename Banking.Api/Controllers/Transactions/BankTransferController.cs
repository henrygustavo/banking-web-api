namespace Banking.Api.Controllers.Transactions
{
    using Banking.Application.Service.Transactions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Banking.Application.Dto.Transactions;

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

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post([FromBody]BankTransferInputDto bankTransfertransfer)
        {

            _transactionApplicationService.PerformTransfer(bankTransfertransfer);
            return Ok("Transfer done!");
           
        }
    }
}