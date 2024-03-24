using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Backlog
{
    public interface IBacklog
    {
        public int Id { get; }
        public User? User { get; }
        public Thread? Thread { get; }
    }
}
