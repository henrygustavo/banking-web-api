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

        public BankAccountDto Get(int id)
        {
            return Mapper.Map<BankAccountDto>(_unitOfWork.BankAccounts.GetWithCustomer(id));
        }

        public NewBankAccountDto GenerateAccountNumber()
        {
            return new NewBankAccountDto {AccountNumber = _unitOfWork.BankAccounts.GenerateAccountNumber()};
        }

        public PaginationResultDto GetAll(int page, int pageSize)
        {
            var entities = _unitOfWork.BankAccounts.GetAllWithCustomers(page, pageSize, "number", "desc").ToList();

            var pagedRecord = new PaginationResultDto
            {
                Content = Mapper.Map<List<BankAccountDto>>(entities),
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
                Balance = 0,
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
