using AutoMapper;
using Data.DTOs;
using Data.Entities;

namespace Web.AutoMapperProfiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, ClaimDto>().ReverseMap();
        }
    }
}
