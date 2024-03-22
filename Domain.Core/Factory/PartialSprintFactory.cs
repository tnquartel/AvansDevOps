using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Factory
{
    class PartialSprintFactory
    {
        public ISprint CreateSprint()
        {
            return new PartialSprint();
        }
    }
}
