using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Sprint;

namespace Domain.Core.Factory.Factory_Interfaces
{

    // Interface for Factory pattern to create sprints.
    interface ISprintFactory
    {
        public ISprint CreateSprint();
    }
}
