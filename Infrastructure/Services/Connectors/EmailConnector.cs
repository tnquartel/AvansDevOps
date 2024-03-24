using Domain.Services.Services.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Connectors
{
    public class EmailConnector : IEmailConnector
    {
        public void SendEmail(string email)
        {
            Console.WriteLine("Succesfully Send Email.");
        }
    }
}
