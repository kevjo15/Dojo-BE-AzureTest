using Application_Layer.DTO_s;
using AutoMapper;
using Domain_Layer.Models.UserModel;

namespace Application_Layer.AutoMaper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDTO, UserModel>();
        }
    }
}
