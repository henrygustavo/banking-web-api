namespace Banking.Application.Dto.Common
{
    using AutoMapper;
    using Banking.Application.Dto.Accounts;
    using Banking.Application.Dto.Customers;
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