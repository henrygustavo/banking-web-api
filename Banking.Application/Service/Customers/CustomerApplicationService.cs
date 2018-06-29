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

        public int Add(CustomerDto entity)
        {
            _unitOfWork.Customers.Add(Mapper.Map<Customer>(entity));
            return _unitOfWork.Complete();
        }

        public int Update(int id, CustomerDto entity)
        {
            var entityObj = _unitOfWork.Customers.Get(id);

            entityObj.Dni = entity.Dni;
            entityObj.FirstName = entity.FirstName;
            entityObj.LastName = entity.LastName;

            _unitOfWork.Customers.Update(entityObj);
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
