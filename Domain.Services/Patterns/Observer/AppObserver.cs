using Domain.Services.Services;
using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Observer
{
    public class AppObserver : IObserver
    {
        private User User { get; set; }
        private IUserService UserService { get; set; }

        public AppObserver(User user, IUserService userService)
        {
            User = user;
            UserService = userService;

        }
        public void Update(string Message)
        {
            UserService.SendMessage(User, Message);
        }
    }
}