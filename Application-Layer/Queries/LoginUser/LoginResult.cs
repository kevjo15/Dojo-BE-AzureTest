
namespace Application_Layer.Queries.LoginUser
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }

    }
}
