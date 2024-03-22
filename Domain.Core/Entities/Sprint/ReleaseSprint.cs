using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Sprint
{
    public class ReleaseSprint : ISprint
    {
        public int Id { get; private set; }
        public ReleaseSprint() { } 
    }
}
