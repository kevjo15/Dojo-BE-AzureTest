
namespace Application_Layer.DTO_s
{
    public class CourseUpdateDTO
    {
        public string Title { get; set; }
        public string CategoryOrSubject { get; set; }
        public string LevelOfDifficulty { get; set; }
        public string PriceOrPriceModel { get; set; }
        public string EnrolmentStatus { get; set; }
        public string Language { get; set; }
        public TimeSpan Duration { get; set; }
        public string? ThumbnailOrImageUrl { get; set; }
        public string? ContentUrl { get; set; }
        public string? Tags { get; set; }
        public string? Prerequisites { get; set; }
        public bool CourseIsPublic { get; set; }
        public bool CourseIsCompleted { get; set; }
        public bool IssueCertificate { get; set; }
    }
}
