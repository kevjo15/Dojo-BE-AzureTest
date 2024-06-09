using Application_Layer.DTO_s;
using Application_Layer.DTO_s.Content;
using Application_Layer.DTO_s.Module;
using AutoMapper;
using Domain_Layer.Models.Content;
using Domain_Layer.Models.Course;
using Domain_Layer.Models.Module;
using Domain_Layer.Models.User;

namespace Application_Layer.AutoMaper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterUserDTO, UserModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<UpdatingUserDTO, UserModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
            CreateMap<CourseUpdateDTO, CourseModel>()
            .ForMember(dest => dest.CourseId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTimestamp, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTimestamp, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<CreateCourseDTO, CourseModel>();
            CreateMap<CreateModuleDTO, ModuleModel>();
            CreateMap<UpdateModuleDTO, ModuleModel>()
            .ForMember(dest => dest.ModuleId, opt => opt.Ignore());
            CreateMap<CreateContentDTO, ContentModel>();
        }
    }
}
