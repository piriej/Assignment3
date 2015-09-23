using AutoMapper;
using Library.Features.CardReader;

namespace Library.ApplicationInfratructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<CardReaderViewModel, CardReaderModel>();

            Mapper.AssertConfigurationIsValid();    
        }
    }
}