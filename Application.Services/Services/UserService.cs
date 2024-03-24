using Domain.Services.Services;
using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class UserService : IUserService
    {

        public void SendMessage(User user, string message)
        {
            Console.WriteLine(user.Name + " : " + message);
        }
    }
}
