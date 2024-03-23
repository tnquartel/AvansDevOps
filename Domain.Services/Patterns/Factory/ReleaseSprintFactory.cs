using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;

namespace Domain.Services.Patterns.Factory
{
    class ReleaseSprintFactory : ISprintFactory
    {
        public ISprint CreateSprint()
        {
            return new ReleaseSprint();
        }
    }
}
