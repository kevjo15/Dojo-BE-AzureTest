using MediatR;

namespace Application_Layer.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
