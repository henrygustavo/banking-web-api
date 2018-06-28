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
        private readonly IMapper _mapper;
        public CustomerApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CustomerDto Get(int id)
        {
            return _mapper.Map<CustomerDto>(_unitOfWork.Customers.Get(id));
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            return _mapper.Map<IEnumerable<CustomerDto>>(_unitOfWork.Customers.GetAll());
        }

        public void Add(CustomerDto entity)
        {
            _unitOfWork.Customers.Add(_mapper.Map<Customer>(entity));
        }

        public void AddRange(IEnumerable<CustomerDto> entities)
        {
            _unitOfWork.Customers.AddRange(_mapper.Map<IEnumerable<Customer>>(entities));
        }

        public void Remove(CustomerDto entity)
        {
            _unitOfWork.Customers.Remove(_mapper.Map<Customer>(entity));
        }

        public void RemoveRange(IEnumerable<CustomerDto> entities)
        {
            _unitOfWork.Customers.RemoveRange(_mapper.Map<IEnumerable<Customer>>(entities));
        }
    }
}
