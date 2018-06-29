namespace Banking.Application.Service.Accounts
{
    using AutoMapper;
    using Banking.Application.Dto.Accounts;
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Repository.Common;
    using System.Collections.Generic;

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

        public void Add(BankAccountDto entity)
        {
            _unitOfWork.BankAccounts.Add(Mapper.Map<BankAccount>(entity));
        }

        public void AddRange(IEnumerable<BankAccountDto> entities)
        {
            _unitOfWork.BankAccounts.AddRange(Mapper.Map<IEnumerable<BankAccount>>(entities));
        }

        public void Remove(BankAccountDto entity)
        {
            _unitOfWork.BankAccounts.Remove(Mapper.Map<BankAccount>(entity));
        }
    }
}
