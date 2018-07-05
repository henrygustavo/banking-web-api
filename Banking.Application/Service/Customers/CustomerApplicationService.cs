namespace Banking.Application.Service.Customers
{
    using AutoMapper;
    using Banking.Application.Dto.Common;
    using Banking.Application.Dto.Customers;
    using Banking.Application.Dto.Transactions;
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Entity.Identities;
    using Banking.Domain.Repository.Common;
    using Banking.Domain.Service.Customers;
    using Banking.Domain.Service.Identities;
    using Notification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityUserDomainService _identityUserDomainService;

        private readonly ICustomerDomainService _customerDomainService;
        public CustomerApplicationService(IUnitOfWork unitOfWork,
            IIdentityUserDomainService identityUserDomainService,
            ICustomerDomainService customerDomainServic)
        {
            _unitOfWork = unitOfWork;
            _identityUserDomainService = identityUserDomainService;
            _customerDomainService = customerDomainServic;
        }

        public CustomerOutputDto Get(int id)
        {
            return Mapper.Map<CustomerOutputDto>(_unitOfWork.Customers.Get(id));
        }

        public List<CustomerBankTransferOtputDto>  GetBankTransfersById(int id)
        {
            var customer = _unitOfWork.Customers.GetByIdWithBankAccounts(id);
            if (customer.BankAccounts == null) return new List<CustomerBankTransferOtputDto>();

            return Mapper.Map<List<CustomerBankTransferOtputDto>>(customer.BankAccounts);
        }
        
        public CustomerIdentityOutputDto GetWithIdentity(int id)
        {
            Customer customer = _unitOfWork.Customers.Get(id);

            if (customer == null) return new CustomerIdentityOutputDto();

            customer.IdentityUser = _unitOfWork.IdentityUsers.Get(customer.IdentityUserId);
            
            return Mapper.Map<CustomerIdentityOutputDto>(customer);
        }

        public CustomerOutputDto GetByDni(string dni)
        { 
            Customer customer = _unitOfWork.Customers.GetByDni(dni);

            return customer == null ? new CustomerOutputDto() : Mapper.Map<CustomerOutputDto>(customer);
        }

        public PaginationOutputDto GetAll(int page, int pageSize)
        {
            var entities = _unitOfWork.Customers.GetAll(page, pageSize, "lastName", "asc").ToList();

            var pagedRecord = new PaginationOutputDto
            {
                Content = Mapper.Map<List<CustomerOutputDto>>(entities),
                TotalRecords = _unitOfWork.Customers.CountGetAll(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public int Add(CustomerInputDto entity)
        {
            Notification notification = ValidationInsert(entity);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            var identityUserWithSameEmail = _unitOfWork.IdentityUsers.GetByEmail(entity.Email);

            var identityUserWithSameUserName = _unitOfWork.IdentityUsers.GetByUserName(entity.UserName);

            var newIdentityUser = new IdentityUser(entity.UserName, entity.Email, entity.Password, "member", entity.Active);

             _identityUserDomainService.PerformNewUser(newIdentityUser, identityUserWithSameEmail, identityUserWithSameUserName);

            _unitOfWork.IdentityUsers.Add(newIdentityUser);

            var customer = Mapper.Map<Customer>(entity);

            var customerWithSameDni = _unitOfWork.Customers.GetByDni(entity.Dni);

            _customerDomainService.PerformNewCustomer(customer, customerWithSameDni, newIdentityUser.Id);
            
            _unitOfWork.Customers.Add(customer);
           
            _unitOfWork.Complete();

            return customer.Id;
        }

        public int Update(int id, CustomerInputUpdateDto entity)
        {
            Notification notification = ValidationUpdate(entity);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            var customer = _unitOfWork.Customers.Get(id);

            var identityUser = _unitOfWork.IdentityUsers.Get(customer?.IdentityUserId ?? 0);

            _customerDomainService.PerformUpdateCustomer(customer, entity.FirstName, entity.LastName,
                                                         entity.Active, identityUser);
            _unitOfWork.Customers.Update(customer);
            _unitOfWork.Complete();

            return customer?.Id ?? 0;
        }

        private Notification ValidationInsert(CustomerInputDto entity)
        {
            Notification notification = new Notification();

            if (entity == null)
            {
                notification.AddError("Invalid JSON data in request body");
                return notification;
            }
           
            if (string.IsNullOrEmpty(entity.Dni))
            {
                notification.AddError("DNI is missing");

            }else if (entity.Dni.Length != 8)
            {
                notification.AddError("DNI should have 8 numbers");
            }

            if (string.IsNullOrEmpty(entity.FirstName))
            {
                notification.AddError("FirstName is missing");
            }
            else if (entity.FirstName.Length < 2)
            {
                notification.AddError("FirstName should have min 2 characters");
            }

            if (string.IsNullOrEmpty(entity.LastName))
            {
                notification.AddError("LastName is missing");
            }
            else if (entity.LastName.Length < 2)
            {
                notification.AddError("LastName should have min 2 characters");
            }

            if (string.IsNullOrEmpty(entity.UserName))
            {
                notification.AddError("UserName is missing");
            }
            else if (entity.UserName.Length < 4)
            {
                notification.AddError("UserName should have min 4 characters");
            }

            if (string.IsNullOrEmpty(entity.Password))
            {
                notification.AddError("Password is missing");
            }
            else if (entity.Password.Length < 6)
            {
                notification.AddError("Password should have min 6 characters");
            }

            if (!IsValidEmail(entity.Email))
            {
                notification.AddError("Email is not valid");
                return notification;
            }

            return notification;
        }

        private Notification ValidationUpdate(CustomerInputUpdateDto entity)
        {
            Notification notification = new Notification();

            if (entity == null)
            {
                notification.AddError("Invalid JSON data in request body.");
                return notification;
            }

            if (string.IsNullOrEmpty(entity.FirstName))
            {
                notification.AddError("FirstName is missing");
            }
            else if (entity.FirstName.Length < 2)
            {
                notification.AddError("FirstName should have min 2 characters");
            }

            if (string.IsNullOrEmpty(entity.LastName))
            {
                notification.AddError("LastName is missing");
            }
            else if (entity.LastName.Length < 2)
            {
                notification.AddError("LastName should have min 2 characters");
            }

            return notification;

        }
        
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
