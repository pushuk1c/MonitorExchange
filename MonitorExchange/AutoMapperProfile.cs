using AutoMapper;
using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FEOffers;
using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Dtos.Goods;
using MonitorExchange.Dtos.GoodsSize;
using MonitorExchange.Dtos.Stock;
using MonitorExchange.Dtos.User;
using MonitorExchange.Models;

namespace MonitorExchange
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //FileExchange
            CreateMap<FileExchange, GetFileExchangeDto>();
            CreateMap<AddFileExchangeDto, FileExchange>();
            CreateMap<UpdateFileExchangeDto, FileExchange>();
            
            //Goods
            CreateMap<Goods, GetGoodsDto>();
            CreateMap<AddGoodsDto, Goods>();
            CreateMap<UpdateGoodsDto, Goods>();
            CreateMap<GoodsXMLDto, Goods>();

            //GoodsSize
            CreateMap<GoodsSize, GetGoodsSizeDto>();
            CreateMap<AddGoodsSizeDto, GoodsSize>();
            CreateMap<UpdateGoodsSizeDto, GoodsSize>();

            //Stock
            CreateMap<Stock, GetStockDto>();
            CreateMap<AddStockDto, Stock>();
            CreateMap<Stock, AddStockDto>();
            CreateMap<UpdateStockDto, Stock>();
            CreateMap<StockXMLDto,Stock>();

            //User
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();

            //FEImport
            CreateMap<FEImport, GetFEImportsDto>()
            .ForMember(dest => dest.GUIDFile, opt => opt.MapFrom(src => src.FileExchange.StrId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Goods.Code))
            .ForMember(dest => dest.Artikul, opt => opt.MapFrom(src => src.Goods.Artikul))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Goods.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Goods.Type))
            .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.Goods.View))
            .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Goods.Manufacturer))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Goods.Country))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Goods.Material))
            .ForMember(dest => dest.Season, opt => opt.MapFrom(src => src.Goods.Season))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Goods.Color))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Goods.Categoria))
            .ForMember(dest => dest.Marke, opt => opt.MapFrom(src => src.Goods.Marke))
            .ForMember(dest => dest.Brend, opt => opt.MapFrom(src => src.Goods.Brend))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Goods.Sex))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.GoodsSize.Name))
            ;

        }
    }
}
