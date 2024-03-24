using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageBody { get; set; }
        public User User { get; set; }

        public Message(string messageBody, User user)
        {
            MessageBody = messageBody;
            User = user;
        }
    }
}
