﻿namespace Banking.Application.Service.Accounts
{
    using AutoMapper;
    using Banking.Application.Dto.Accounts;
    using Banking.Application.Dto.Common;
    using Banking.Domain.Repository.Common;
    using Banking.Domain.Service.Accounts;
    using Notification;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BankAccountApplicationService : IBankAccountApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountDomainService _bankAccountDomainService;

        public BankAccountApplicationService(IUnitOfWork unitOfWork, IBankAccountDomainService bankAccountDomainService)
        {
            _unitOfWork = unitOfWork;
            _bankAccountDomainService = bankAccountDomainService;

        }

        public BankAccountOutputDto Get(int id)
        {
            return Mapper.Map<BankAccountOutputDto>(_unitOfWork.BankAccounts.GetWithCustomer(id));
        }

        public BankAccountNumberOutputDto GenerateAccountNumber()
        {
            return new BankAccountNumberOutputDto {AccountNumber = _unitOfWork.BankAccounts.GenerateAccountNumber()};
        }

        public BankAccountNumberOutputDto GetAccountNumber(int id)
        {
            return new BankAccountNumberOutputDto { AccountNumber = _unitOfWork.BankAccounts.Get(id).Number };
        }

        public PaginationOutputDto GetAll(int page, int pageSize, string sortBy, string sortDirection)
        {
            var entities = _unitOfWork.BankAccounts.GetAllWithCustomers(page, pageSize, sortBy, sortDirection).ToList();

            var pagedRecord = new PaginationOutputDto
            {
                Content = Mapper.Map<List<BankAccountOutputDto>>(entities),
                TotalRecords = _unitOfWork.BankAccounts.CountGetAll(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public int Add(BankAccountInputDto entity)
        {
            Notification notification = Validation(entity);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            var customer = _unitOfWork.Customers.GetByIdWithBankAccounts(entity.CustomerId);

            var bankAccountWithSameAccountNumber = _unitOfWork.BankAccounts.GetByNumber(entity.Number);
            
            _bankAccountDomainService.PerformNewBankAccount(customer, bankAccountWithSameAccountNumber,
                                                            entity.Number, entity.IsLocked);
    
            return _unitOfWork.Complete();
        }

        public int Update(int id, BankAccountInputUpdateDto entity)
        {
            Notification notification = ValidationUpdate(entity);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            var bankAccount = _unitOfWork.BankAccounts.Get(id);

            _bankAccountDomainService.PerformUpdateBankAccount(bankAccount, entity.IsLocked);
            _unitOfWork.BankAccounts.Update(bankAccount);

            return _unitOfWork.Complete();
        }

        private Notification Validation(BankAccountInputDto entity)
        {
            Notification notification = new Notification();

            if (entity == null)
            {
                notification.AddError("Invalid JSON data in request body.");

                return notification;
            }
           
            if (string.IsNullOrEmpty(entity.Number))
            {
                notification.AddError("Bank Account Number is missing");

            }
            else if (entity.Number.Length != 18)
            {
                notification.AddError("Bank Account Number should have 18 numbers");
            }
            return notification;
        }

        private Notification ValidationUpdate(BankAccountInputUpdateDto entity)
        {
            Notification notification = new Notification();

            if (entity == null)
            {
                notification.AddError("Invalid JSON data in request body.");

                return notification;
            }

            return notification;
        }
    }
}
