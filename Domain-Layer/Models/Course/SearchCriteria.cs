using System.ComponentModel.DataAnnotations;

namespace Domain_Layer.Models.Course
{
    public class SearchCriteria
    {
        public bool? SearchBySearchTerm { get; set; } = true;
        public string? CourseId { get; set; }

        [DataType(DataType.Text)]
        public string? Title { get; set; }

        [DataType(DataType.Text)]
        public string? CategoryOrSubject { get; set; }

        [DataType(DataType.Text)]
        public string? Language { get; set; }

        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        public string? LastName { get; set; }
    }
}
