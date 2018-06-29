namespace Banking.Application.Service.Customers
{
    using AutoMapper;
    using Banking.Application.Dto.Customers;
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Repository.Common;
    using System.Collections.Generic;
    using System.Linq;
    using Banking.Application.Dto.Common;

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

        public PaginationResultDto GetAll(int page, int pageSize)
        {
            var entities = _unitOfWork.Customers.GetAll(page, pageSize, "lastName", "asc").ToList();

            var pagedRecord = new PaginationResultDto
            {
                Content = Mapper.Map<List<CustomerDto>>(entities),
                TotalRecords = _unitOfWork.Customers.CountGetAll(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public int Add(CustomerDto entity)
        {
            var entityObj = Mapper.Map<Customer>(entity);
            _unitOfWork.Customers.Add(entityObj);
            _unitOfWork.Complete();

            return entityObj.Id;
        }

        public int Update(int id, CustomerDto entity)
        {
            var entityObj = _unitOfWork.Customers.Get(id);

            entityObj.Dni = entity.Dni;
            entityObj.FirstName = entity.FirstName;
            entityObj.LastName = entity.LastName;

            _unitOfWork.Customers.Update(entityObj);
            _unitOfWork.Complete();

            return entityObj.Id;
        }
        public int Remove(int id)
        {
            var entity = _unitOfWork.Customers.Get(id);
            _unitOfWork.Customers.Remove(entity);
            return _unitOfWork.Complete();
        }
    }
}
