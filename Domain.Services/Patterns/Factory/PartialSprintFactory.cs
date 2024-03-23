using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Factory
{
    class PartialSprintFactory : ISprintFactory
    {
        public ISprint CreateSprint()
        {
            return new PartialSprint();
        }
    }
}
