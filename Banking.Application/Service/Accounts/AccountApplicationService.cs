namespace Banking.Application.Service.Accounts
{
    using AutoMapper;
    using Banking.Application.Dto.Accounts;
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Repository.Common;
    using System.Collections.Generic;
    using System.Linq;
    using Banking.Application.Dto.Common;

    public class AccountApplicationService : IAccountApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

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

        public PaginationOutputDto GetAll(int page, int pageSize)
        {
            var entities = _unitOfWork.BankAccounts.GetAllWithCustomers(page, pageSize, "number", "desc").ToList();

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
            var customer = _unitOfWork.Customers.GetByIdWithBankAccounts(entity.CustomerId);

            customer.BankAccounts.Add(new BankAccount
            {
                Number = entity.Number,
                Balance = 1000,//just for demo
                IsLocked = entity.IsLocked
            });

            return _unitOfWork.Complete();
        }

        public int Update(int id, BankAccountInputDto entity)
        {
            var entityObj = _unitOfWork.BankAccounts.Get(id);

            entityObj.IsLocked = entity.IsLocked;
            _unitOfWork.BankAccounts.Update(entityObj);

            return _unitOfWork.Complete();
        }
    }
}
