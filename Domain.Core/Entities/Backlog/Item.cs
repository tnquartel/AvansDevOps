using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Backlog
{
    public class Item : IBacklog
    {
        public int Id { get; private set; }
        public Thread Thread { get; private set; }

        public List<Activity> Activities { get; private set; }

    }
}
