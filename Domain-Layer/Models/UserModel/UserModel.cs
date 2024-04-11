using Microsoft.AspNetCore.Identity;

namespace Domain_Layer.Models.UserModel
{
    public class UserModel : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
