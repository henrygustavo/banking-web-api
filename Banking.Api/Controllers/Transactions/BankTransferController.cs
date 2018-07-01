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

    [Produces("application/json")]
    [Route("api/bankTransfers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Member")]
    public class BankTransferController : Controller
    {
        private readonly ITransactionApplicationService _transactionApplicationService;
        private readonly ICustomerApplicationService _customerApplicationService;

        public BankTransferController(ITransactionApplicationService transactionApplicationService,
                                      ICustomerApplicationService customerApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
            _customerApplicationService = customerApplicationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerBankTransferOtputDto>), 200)]
        public IActionResult GetAll()
        {
            var clientId = User.Claims.FirstOrDefault(p => p.Type == "customerId");

            if (clientId == null) return BadRequest("something went wrong with the credentials");

            return Ok(_customerApplicationService.GetBankTransfersById(int.Parse(clientId.Value)));
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