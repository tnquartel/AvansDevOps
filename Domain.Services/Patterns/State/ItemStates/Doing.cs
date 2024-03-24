using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Backlog;

namespace Domain.Services.Patterns.State.ItemStates
{
    public class Doing : IStateItem
    {
        public Item Item { get; set; }

        public Doing(Item item)
        {
            Item = item;
        }

        public void NextState()
        {
            Item.State = new ReadyForTesting(Item);
        }
    }
}
