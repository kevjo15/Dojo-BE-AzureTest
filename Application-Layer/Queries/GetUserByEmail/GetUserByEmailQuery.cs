using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Models.UserModel;
using MediatR;

namespace Application_Layer.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserModel>
    {
        public string Email { get; private set; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
