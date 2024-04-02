using System.ComponentModel.DataAnnotations;

namespace Application_Layer.DTO_s
{
    public class UpdatingUserDTO
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(Admin|Teacher|Student)$", ErrorMessage = "Invalid role.")]
        public string Role { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = string.Empty;
    }
}
