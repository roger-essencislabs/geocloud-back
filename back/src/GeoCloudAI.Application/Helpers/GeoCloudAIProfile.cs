using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Helpers
{
    public class GeoCloudAIProfile : AutoMapper.Profile
    {
        public GeoCloudAIProfile()
        {
            CreateMap<Account,               AccountDto>().ReverseMap();
            CreateMap<Country,               CountryDto>().ReverseMap();
            CreateMap<Profile,               ProfileDto>().ReverseMap();
            CreateMap<User,                  UserDto>().ReverseMap();
        }
    }
}