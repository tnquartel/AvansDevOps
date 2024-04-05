using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IAuthService
    {
        public void LogIn(User user);
        public User GetLoggedInUser();
    }
}
