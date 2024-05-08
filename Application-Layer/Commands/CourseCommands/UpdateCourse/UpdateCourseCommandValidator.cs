using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application_Layer.Commands.CourseCommands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required.")
                .Length(36, 36).WithMessage("CourseId must be a valid GUID.");

            RuleFor(x => x.CourseUpdateDTO.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");

            RuleFor(x => x.CourseUpdateDTO.CategoryOrSubject)
                .NotEmpty().WithMessage("Category or Subject is required.");

            RuleFor(x => x.CourseUpdateDTO.LevelOfDifficulty)
                .NotEmpty().WithMessage("Level of Difficulty is required.");

            RuleFor(x => x.CourseUpdateDTO.PriceOrPriceModel)
                .NotEmpty().WithMessage("Price or Price Model is required.");

            RuleFor(x => x.CourseUpdateDTO.EnrolmentStatus)
                .NotEmpty().WithMessage("Enrolment Status is required.");

            RuleFor(x => x.CourseUpdateDTO.Language)
                .NotEmpty().WithMessage("Language is required.");

            RuleFor(x => x.CourseUpdateDTO.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.CourseUpdateDTO.ThumbnailOrImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("Thumbnail or Image URL must be a valid URL.");

            RuleFor(x => x.CourseUpdateDTO.ContentUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("Content URL must be a valid URL.");

            RuleFor(x => x.CourseUpdateDTO.Tags)
                .NotEmpty().WithMessage("Tags are required.");

            RuleFor(x => x.CourseUpdateDTO.Prerequisites)
                .NotEmpty().WithMessage("Prerequisites are required.");

            RuleFor(x => x.CourseUpdateDTO.CourseIsPublic)
                .NotNull().WithMessage("CourseIsPublic is required.");

            RuleFor(x => x.CourseUpdateDTO.CourseIsCompleted)
                .NotNull().WithMessage("CourseIsCompleted is required.");

            RuleFor(x => x.CourseUpdateDTO.IssueCertificate)
                .NotNull().WithMessage("IssueCertificate is required.");
        }
    }
}
