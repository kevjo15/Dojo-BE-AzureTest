using System.ComponentModel.DataAnnotations;

namespace Application_Layer.DTO_s
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
