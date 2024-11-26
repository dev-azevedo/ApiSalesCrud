using AutoMapper;
using SalesCrud.Model;
using SalesCrud.ViewModel;

namespace SalesCrud.Config;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Product, ProductPostViewModel>().ReverseMap();
        CreateMap<Product, ProductPutViewModel>().ReverseMap();
        CreateMap<Product, ProductRespViewModel>().ReverseMap();

        CreateMap<Client, ClientPostViewModel>().ReverseMap();
        CreateMap<Client, ClientPutViewModel>().ReverseMap();
        CreateMap<Client, ClientRespViewModel>().ReverseMap();

        CreateMap<Sale, SalePostViewModel>().ReverseMap();
        CreateMap<Sale, SalePutViewModel>().ReverseMap();
        CreateMap<Sale, SaleRespViewModel>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
    }
}
