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
            return Mapper.Map<BankAccountDto>(_unitOfWork.BankAccounts.Get(id));
        }

        public IEnumerable<BankAccountDto> GetAll()
        {
            return Mapper.Map<IEnumerable<BankAccountDto>>(_unitOfWork.BankAccounts.GetAll());
        }

        public PaginationResultDto GetAll(int page, int pageSize)
        {
            var entities = _unitOfWork.Customers.GetAll(page, pageSize, "number", "asc").ToList();

            var pagedRecord = new PaginationResultDto
            {
                Content = Mapper.Map<List<BankAccountDto>>(entities),
                TotalRecords = _unitOfWork.BankAccounts.CountGetAll(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public int Add(BankAccountDto entity)
        {
            _unitOfWork.BankAccounts.Add(Mapper.Map<BankAccount>(entity));
            return _unitOfWork.Complete();
        }

        public int Update(int id, BankAccountDto entity)
        {
            var entityObj = _unitOfWork.BankAccounts.Get(id);

            _unitOfWork.BankAccounts.Update(entityObj);
            return _unitOfWork.Complete();
        }

        public int Remove(int id)
        {
            var entity = _unitOfWork.Customers.Get(id);
            _unitOfWork.Customers.Remove(entity);
            return _unitOfWork.Complete();
        }
    }
}
