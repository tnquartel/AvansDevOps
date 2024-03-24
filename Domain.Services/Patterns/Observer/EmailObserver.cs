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

        public EmailObserver(User user) {
            User = user;
        }

        public void Update(string Message)
        {
            Console.WriteLine(User + " : " + Message);
        }
    }
}
