using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Backlog
{
    public class Item : IBacklog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User? User { get; set; }
        public MessageThread? Thread { get; set; }
        public List<ActivityItem> Activities { get; set; }
        public IStateItem State { get; set; }
        public Project? project { get; set; }

        public Item(IStateItem state, string name) { 
            Activities = new List<ActivityItem>();
            State = state;
            Name = name;
        }

    }   
}
