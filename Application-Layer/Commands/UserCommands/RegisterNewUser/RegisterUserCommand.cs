using Application_Layer.DTO_s;
using Domain_Layer.Models.User;
using MediatR;

namespace Application_Layer.Commands.RegisterNewUser
{
    public class RegisterUserCommand : IRequest<UserModel>
    {
        public RegisterUserDTO NewUser { get; }
        public RegisterUserCommand(RegisterUserDTO newUser)
        {
            NewUser = newUser;
        }
    }
}
