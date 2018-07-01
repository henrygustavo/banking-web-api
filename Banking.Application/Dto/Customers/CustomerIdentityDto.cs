using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Application.Dto.Customers
{
    public class CustomerIdentityDto
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public bool Active { get; set; }
    }
}
