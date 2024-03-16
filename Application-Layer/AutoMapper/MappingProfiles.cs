using Application_Layer.DTO_s;
using AutoMapper;
using Domain_Layer.Models.UserModel;

namespace Application_Layer.AutoMaper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterUserDTO, UserModel>();
            CreateMap<UpdatingUserDTO, UserModel>();
        }
    }
}
