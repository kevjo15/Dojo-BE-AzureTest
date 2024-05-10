using Domain_Layer.Models.User;
using MediatR;

namespace Application_Layer.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserModel>>
    {
    }
}
