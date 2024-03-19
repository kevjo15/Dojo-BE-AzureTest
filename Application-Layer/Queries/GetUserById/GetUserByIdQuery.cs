using Domain_Layer.Models.UserModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application_Layer.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserModel>
    {
        public string UserId { get; private set; }
        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
