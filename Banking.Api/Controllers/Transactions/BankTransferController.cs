namespace Banking.Api.Controllers.Transactions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/BankTransfer")]
    public class BankTransferController : Controller
    {

        public BankTransferController()
        {

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}