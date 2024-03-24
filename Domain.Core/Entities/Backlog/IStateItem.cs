using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Backlog
{
    public interface IStateItem
    {
        // State Interface for State Pattern
        public Item Item { get; set; }
        public void NextState();
    }
}
