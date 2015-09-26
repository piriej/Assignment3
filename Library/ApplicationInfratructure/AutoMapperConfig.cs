using AutoMapper;
using Library.Entities;
using Library.Features.Borrowing;
using Library.Features.CardReader;

namespace Library.ApplicationInfratructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<CardReaderViewModel, CardReaderModel>();
            Mapper.CreateMap<Member, BorrowingModel>();

            Mapper.AssertConfigurationIsValid();    
        }
    }
}