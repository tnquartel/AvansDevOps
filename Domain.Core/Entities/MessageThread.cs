using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class MessageThread
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Particapants { get; set; }
        public Item ParentItem { get; set; }
        public ActivityItem ParentActivityItem { get; set; }
    }
}
