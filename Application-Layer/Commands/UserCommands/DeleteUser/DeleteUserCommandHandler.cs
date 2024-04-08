using Infrastructure_Layer.Repositories.User;
using MediatR;

namespace Application_Layer.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _userRepository.DeleteUserByIdAsync(request.UserId);
                if (!success)
                {
                    throw new InvalidOperationException("Failed to delete the user.");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
