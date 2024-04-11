using MediatR;

namespace Application_Layer.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string UserId { get; private set; }
        public DeleteUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
