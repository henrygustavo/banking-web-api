namespace Banking.Application.Dto.Common
{
    using AutoMapper;
    using Accounts;
    using Customers;
    using Banking.Domain.Entity.Customers;
    using Banking.Domain.Entity.Accounts;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerInputDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>()
                .ForMember(destination => destination.FullName,
                    opts => opts.MapFrom(source => $"{source.LastName} {source.FirstName}"));

            CreateMap<Customer, CustomerIdentityDto>()
                .ForMember(destination => destination.UserName,
                    opts => opts.MapFrom(source =>
                        source.IdentityUser != null ? source.IdentityUser.UserName : string.Empty))
                .ForMember(destination => destination.Email,
                    opts => opts.MapFrom(source =>
                        source.IdentityUser != null ? source.IdentityUser.Email : string.Empty));

            CreateMap<BankAccount, BankAccountDto>()
                .ForMember(destination => destination.CustomerFullName,
                    opts => opts.MapFrom(source => source.Customer != null ?
                          source.Customer.Dni : string.Empty))
                .ForMember(destination => destination.CustomerFullName,
                    opts => opts.MapFrom(source => source.Customer != null ? 
                          $"{source.Customer.LastName} {source.Customer.FirstName}" : string.Empty));

            CreateMap<BankAccount, BankAccountInputDto>().ReverseMap();
        }
    }
}