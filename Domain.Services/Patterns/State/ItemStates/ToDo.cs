using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Backlog;

namespace Domain.Services.Patterns.State.ItemStates
{
    public class ToDo : IStateItem
    {
        public Item? Item { get ; set; }

        public ToDo() { 
        }

        public void NextState()
        {
            if(Item != null)
            {
                Item.State = new Doing(Item);
            }
            else
            {
                Console.WriteLine("States Disconnected");
            }
        }

        public IStateItem GetState()
        {
            return this;
        }
    }
}
