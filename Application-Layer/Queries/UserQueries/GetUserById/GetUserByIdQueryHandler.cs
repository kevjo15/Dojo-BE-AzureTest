using Domain_Layer.Models.User;
using Infrastructure_Layer.Repositories.User;
using MediatR;

namespace Application_Layer.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new ArgumentException("UserId cannot be empty.");
            }

            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.UserId} was not found!");
            }

            return user;
        }
    }
}
