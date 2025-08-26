using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Helpers
{
    /// <summary>
    /// This class represents the AutoMapper profile for GeoCloudAI, defining mappings between domain models and DTOs.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class GeoCloudAIProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCloudAIProfile"/> class.
        /// </summary>
        public GeoCloudAIProfile()
        {
            CreateMap<Account,               AccountDto>().ReverseMap();
            CreateMap<Country,               CountryDto>().ReverseMap();
            CreateMap<Profile,               ProfileDto>().ReverseMap();
            CreateMap<User,                  UserDto>().ReverseMap();
            CreateMap<Invoices,              InvoiceDto>().ReverseMap();
        }
    }
}