using AutoMapper;
using Infrastructure_Layer.Repositories.Course;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application_Layer.Commands.CourseCommands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, IActionResult>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
                if (course == null)
                {
                    return new NotFoundObjectResult($"Course with ID {request.CourseId} not found.");
                }

                _mapper.Map(request.CourseUpdateDTO, course);
                await _courseRepository.UpdateCourseAsync(course);

                return new OkObjectResult(course);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"An error occurred while updating the course: {ex.Message}");
            }
        }
    }
}
