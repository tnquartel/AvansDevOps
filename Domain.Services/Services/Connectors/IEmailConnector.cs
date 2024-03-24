using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services.Connectors
{
    public interface IEmailConnector
    {
        public void SendEmail(string email);
    }
}
