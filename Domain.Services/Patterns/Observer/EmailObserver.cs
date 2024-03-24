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
        private IEmailConnector EmailConnector;

        public EmailObserver(IEmailConnector emailConnector) {
            EmailConnector = emailConnector;
        }

        public void Update(string Message)
        {
            EmailConnector.SendEmail(Message);
        }
    }
}
