using AutoMapper;
using Library.Entities;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using Library.Features.ScanBook;

namespace Library.ApplicationInfratructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<CardReaderViewModel, CardReaderModel>();
            Mapper.CreateMap<Member, BorrowingModel>();
            Mapper.CreateMap<IBorrowingModel, ScanBookViewModel>()
                .ForMember(dest => dest.BorrowerId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.ContactPhone));
                
            //Mapper.AssertConfigurationIsValid();    
        }
    }
}