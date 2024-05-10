using AutoMapper;
using Domain_Layer.Models.User;
using Infrastructure_Layer.Repositories.User;
using MediatR;

namespace Application_Layer.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetUserByEmailAsync(request.UpdatingUserInfo.Email);
            if (userToUpdate == null)
            {
                throw new ArgumentNullException($"User with E-mail {request.UpdatingUserInfo.Email} does not exist in the system!");
            }
            try
            {
                _mapper.Map(request.UpdatingUserInfo, userToUpdate);
                var updatedUser = await _userRepository.UpdateUserAsync(userToUpdate, request.UpdatingUserInfo.CurrentPassword, request.UpdatingUserInfo.NewPassword);

                return updatedUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
