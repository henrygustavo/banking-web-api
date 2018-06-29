namespace Banking.Application.Dto.Common
{
    using AutoMapper;
    using Accounts;
    using Customers;
    using Banking.Domain.Entity.Customers;
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<BankAccountDto, BankAccountDto>().ReverseMap();
        }
    }
}