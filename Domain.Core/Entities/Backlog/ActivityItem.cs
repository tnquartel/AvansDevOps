using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Backlog
{
    public class ActivityItem : IBacklog
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public MessageThread? Thread { get; set; }
        public Item parent { get; set; }

        public ActivityItem(Item parent)
        {
            this.parent = parent;
        }
    }
}
