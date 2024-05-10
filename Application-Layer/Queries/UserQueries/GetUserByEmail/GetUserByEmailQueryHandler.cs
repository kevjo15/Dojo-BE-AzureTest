using Domain_Layer.Models.User;
using Infrastructure_Layer.Repositories.User;
using MediatR;

namespace Application_Layer.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email cannot be empty!");
            }

            try
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with Email '{request.Email}' cannot be found!.");
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
