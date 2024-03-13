using Domain_Layer.Models.UserModel;
using MediatR;

namespace Application_Layer.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserModel>>
    {
    }
}
