using Domain_Layer.Models.User;
using MediatR;

namespace Application_Layer.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserModel>
    {
        public string Email { get; private set; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
