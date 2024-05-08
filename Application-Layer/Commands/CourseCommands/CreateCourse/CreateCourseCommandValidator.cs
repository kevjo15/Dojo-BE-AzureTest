using Application_Layer.DTO_s;
using FluentValidation;

namespace Application_Layer.Commands.CourseCommands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.CreateCourseDTO.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");

            RuleFor(x => x.CreateCourseDTO.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.CreateCourseDTO.CategoryOrSubject)
                .NotEmpty().WithMessage("Category or Subject is required.");

            RuleFor(x => x.CreateCourseDTO.LevelOfDifficulty)
                .NotEmpty().WithMessage("Level of Difficulty is required.");

            RuleFor(x => x.CreateCourseDTO.PriceOrPriceModel)
                .NotEmpty().WithMessage("Price or Price Model is required.");

            RuleFor(x => x.CreateCourseDTO.EnrolmentStatus)
                .NotEmpty().WithMessage("Enrolment Status is required.");

            RuleFor(x => x.CreateCourseDTO.Language)
                .NotEmpty().WithMessage("Language is required.");

            RuleFor(x => x.CreateCourseDTO.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.CreateCourseDTO.AverageRating)
                .InclusiveBetween(0, 5).WithMessage("Average Rating must be between 0 and 5.");

            RuleFor(x => x.CreateCourseDTO.CreationTimestamp)
                .NotEmpty().WithMessage("Creation Timestamp is required.");

            RuleFor(x => x.CreateCourseDTO.CourseIsPublic)
                .NotNull().WithMessage("CourseIsPublic is required.");

            RuleFor(x => x.CreateCourseDTO.CourseIsCompleted)
                .NotNull().WithMessage("CourseIsCompleted is required.");

            RuleFor(x => x.CreateCourseDTO.IssueCertificate)
                .NotNull().WithMessage("IssueCertificate is required.");
        }
    }
}
