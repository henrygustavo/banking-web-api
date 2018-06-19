using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers.Transactions
{
    [Produces("application/json")]
    [Route("api/BankTransfer")]
    public class BankTransferController : Controller
    {
    }
}