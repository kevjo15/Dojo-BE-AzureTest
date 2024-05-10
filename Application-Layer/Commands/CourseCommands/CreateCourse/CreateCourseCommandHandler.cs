using Application_Layer.Commands.CourseCommands.CreateCourse;
using AutoMapper;
using Domain_Layer.Models.Course;
using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Commands.CourseCommands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseResult>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<CreateCourseResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var courseModel = _mapper.Map<CourseModel>(request.CreateCourseDTO);
                await _courseRepository.AddCourseAsync(courseModel);
                return new CreateCourseResult { Success = true, Message = "Course successfully created" };
            }
            catch (Exception ex)
            {
                return new CreateCourseResult { Success = false, Message = "An error occurred: " + ex.Message };
            }
        }
    }
}
