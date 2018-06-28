namespace Banking.Application.Service.Customers
{
    using AutoMapper;
    using Banking.Application.Dto.Customers;
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Repository.Common;
    using System.Collections.Generic;
  
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomerDto Get(int id)
        {
            return Mapper.Map<CustomerDto>(_unitOfWork.Customers.Get(id));
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            return Mapper.Map<IEnumerable<CustomerDto>>(_unitOfWork.Customers.GetAll());
        }

        public void Add(CustomerDto entity)
        {
            _unitOfWork.Customers.Add(Mapper.Map<Customer>(entity));
        }

        public void AddRange(IEnumerable<CustomerDto> entities)
        {
            _unitOfWork.Customers.AddRange(Mapper.Map<IEnumerable<Customer>>(entities));
        }

        public void Remove(CustomerDto entity)
        {
            _unitOfWork.Customers.Remove(Mapper.Map<Customer>(entity));
        }

        public void RemoveRange(IEnumerable<CustomerDto> entities)
        {
            _unitOfWork.Customers.RemoveRange(Mapper.Map<IEnumerable<Customer>>(entities));
        }
    }
}
