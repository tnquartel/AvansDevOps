using Domain.Core.Entities;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class AuthService : IAuthService
    {
        private User? loggedInUser { get; set; }

        public void LogIn(User user)
        {
            loggedInUser = user;
        }

        public User GetLoggedInUser()
        {
            return loggedInUser;
        }
    }
}
