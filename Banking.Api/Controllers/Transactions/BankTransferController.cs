namespace Banking.Api.Controllers.Transactions
{
    using Banking.Application.Service.Transactions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Banking.Application.Dto.Transactions;
    using System.Collections.Generic;
    using System.Linq;
    using Banking.Application.Service.Customers;
    using Banking.Application.Dto.Accounts;
    using Banking.Application.Service.Accounts;

    [Produces("application/json")]
    [Route("api/bankTransfers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Member")]
    public class BankTransferController : Controller
    {
        private readonly ITransactionApplicationService _transactionApplicationService;
        private readonly ICustomerApplicationService _customerApplicationService;
        private readonly IAccountApplicationService _accountApplicationService;

        public BankTransferController(ITransactionApplicationService transactionApplicationService,
                                      ICustomerApplicationService customerApplicationService,
                                      IAccountApplicationService accountApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
            _customerApplicationService = customerApplicationService;
            _accountApplicationService = accountApplicationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerBankTransferOtputDto>), 200)]
        public IActionResult GetAll()
        {
            var clientId = User.Claims.FirstOrDefault(p => p.Type == "customerId");

            if (clientId == null) return BadRequest("something went wrong with the credentials");

            return Ok(_customerApplicationService.GetBankTransfersById(int.Parse(clientId.Value)));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BankAccountNumberOutputDto), 200)]
        public IActionResult GetAccountNumberById(int id)
        {
            return Ok(_accountApplicationService.GetAccountNumber(id));
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