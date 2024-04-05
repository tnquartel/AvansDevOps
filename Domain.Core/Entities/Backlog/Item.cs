using Domain.Core.Entities.Sprint;
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
        public User? User { get; set; }
        public Project Project { get; set; }
        public ISprint Sprint { get; set; }
        public MessageThread? Thread { get; set; }
        public List<ActivityItem> Activities { get; set; }
        public IStateItem State { get; set; }

        public Item(IStateItem state, Project project) { 
            Activities = new List<ActivityItem>();
            State = state;
            Project = project;
        }

    }   
}
