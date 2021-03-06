﻿namespace Banking.Domain.Entity.Identities
{
    using Customers;
    using System;
    using System.Security.Claims;
    using System.Text;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Cryptography;

    public class IdentityUser
    {
        public  int Id { get; set; }
        public  string UserName { get; set; }
        public  string Email { get; set; }

        public  string Role { get; set; }
        public  string Password { get; set; }
        public bool Active { get; set; }
        public Customer Customer { get; set; }

        public IdentityUser()
        {

        }

        public IdentityUser(string userName, string email, string password,string role, bool active)
        {
            UserName = userName;
            Email = email;
            Password = HashPassword(password);
            Active = active;
            Role = role;
        }

        public bool HasValidCredentials(string userName, string password)
        {
            return UserName == userName && Password == HashPassword(password);
        }

        public string BuildToken(string jwKey, string jwIssuer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int customerId = this.Customer?.Id ?? 0;
            var claims = new[]
            {
                new Claim("customerId", customerId.ToString()),
                new Claim("userName", UserName),
                new Claim("role", Role),
                new Claim(ClaimTypes.Role, Role)
            };

            var token = new JwtSecurityToken(jwIssuer,
                jwIssuer,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private  string HashPassword(string password)
        {
            string hashedPassword;
 
            using (var sha256 = SHA256.Create())
            {
 
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
 
                hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
  
            }

            return hashedPassword;
        }
    }
}
