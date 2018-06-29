﻿namespace Banking.Api.Controllers.Customers
{
    using Banking.Application.Dto.Customers;
    using Banking.Application.Service.Customers;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomersController(ICustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerApplicationService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_customerApplicationService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CustomerDto customer)
        {
            return Ok(_customerApplicationService.Add(customer));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerDto customer)
        {
            return Ok(_customerApplicationService.Update(id, customer));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(_customerApplicationService.Remove(id));
        }
    }
}