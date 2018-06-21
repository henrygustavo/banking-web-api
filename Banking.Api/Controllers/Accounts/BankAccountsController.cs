namespace Banking.Api.Controllers.Accounts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/Accounts")]
    public class BankAccountsController : Controller
    {
        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Accounts
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
