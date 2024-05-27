using Domain.Core.Entities;
using Domain.Services.Services.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Observer
{
    public class EmailObserver : IObserver
    {
        private User User { get; set; }
        private String email { get; set; }

        public EmailObserver(User user, string email)
        {
            User = user;
            this.email = email;
        }

        public void Update(string Message)
        {
            Console.WriteLine(User + " : " + Message);
        }
    }
}
