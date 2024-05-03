using Domain.Core.Entities;
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
        private const DevPipe DevPipe = null;

        public ISprint CreateSprint( string goal, ISprintState state, ISubject subject, Project project)
        {
            Console.WriteLine("Created Partial Sprint");

            return new PartialSprint(goal, state, subject, project);

        }
    }
}
