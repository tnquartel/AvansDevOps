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
        public Thread? Thread { get; set; }
        public List<Activity> Activities { get; set; }
        public IStateItem State { get; set; }

        public Item( int id,  IStateItem state) { 
            Id = id;
            Activities = new List<Activity>();
            State = state;
        }

    }   
}
