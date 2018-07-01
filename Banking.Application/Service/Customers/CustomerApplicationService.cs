namespace Banking.Application.Service.Customers
{
    using AutoMapper;
    using Banking.Application.Dto.Customers;
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Repository.Common;
    using System.Collections.Generic;
    using System.Linq;
    using Banking.Application.Dto.Common;
    using Banking.Domain.Entity.Identities;

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

        public CustomerIdentityDto GetWithIdentity(int id)
        {
            Customer customer = _unitOfWork.Customers.Get(id);

            if (customer == null) return new CustomerIdentityDto();

            customer.IdentityUser = _unitOfWork.IdentityUsers.Get(customer.IdentityUserId);
            
            return Mapper.Map<CustomerIdentityDto>(customer); ;
        }

        public CustomerDto GetByDni(string dni)
        { 
            Customer customer = _unitOfWork.Customers.GetByDni(dni);

            return customer == null ? new CustomerDto() : Mapper.Map<CustomerDto>(customer);
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

        public int Add(CustomerInputDto entity)
        {
            var identityUser = new IdentityUser
            {
                UserName = entity.UserName,
                Email = entity.Email,
                Password = entity.Password,
                Role = "member",
                Active = true
            };

            _unitOfWork.IdentityUsers.Add(identityUser);

            var entityObj = Mapper.Map<Customer>(entity);

            entityObj.IdentityUserId = identityUser.Id;

            _unitOfWork.Customers.Add(entityObj);
           
            _unitOfWork.Complete();

            return entityObj.Id;
        }

        public int Update(int id, CustomerInputDto entity)
        {
            var entityObj = _unitOfWork.Customers.Get(id);

            entityObj.FirstName = entity.FirstName;
            entityObj.LastName = entity.LastName;
            entityObj.Active = entity.Active;
            entityObj.IdentityUser = _unitOfWork.IdentityUsers.Get(entityObj.IdentityUserId);

            if(entityObj.IdentityUser != null)
            entityObj.IdentityUser.Active = entity.Active;

            _unitOfWork.Customers.Update(entityObj);
            _unitOfWork.Complete();

            return entityObj.Id;
        }
    }
}
