using AutoMapper;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using MediatR;
using Serilog;

namespace Application_Layer.Commands.RegisterNewUser
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.NewUser == null)
            {
                throw new ArgumentNullException("Invalid user data. FirstName,LastName,Email and Password are required.");
            }
            try
            {
                var userToCreate = _mapper.Map<UserModel>(request.NewUser);
                userToCreate.Role = "Admin";

                var createdUser = await _userRepository.RegisterUserAsync(userToCreate);

                return createdUser;
            }
            catch (Exception ex)
            {

                Log.Error("An error occurred while registering the user.", ex);
                throw;
            }
        }
    }
}
