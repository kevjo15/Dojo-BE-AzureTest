using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
