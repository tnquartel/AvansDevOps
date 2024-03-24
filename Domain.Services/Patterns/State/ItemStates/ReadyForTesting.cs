using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Backlog;

namespace Domain.Services.Patterns.State.ItemStates
{
    public class ReadyForTesting : IStateItem
    {
        public Item Item { get; set; }

        public ReadyForTesting(Item item)
        {
            Item = item;
        }

        public void NextState()
        {
            Item.State = new Testing(Item);
        }
    }
}
