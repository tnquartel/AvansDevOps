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

        public ISprint CreateSprint( int Id, Report Report, string Goal, DevPipe DevPipe = DevPipe)
        {
            Console.WriteLine("Created Partial Sprint");

            return new PartialSprint(Id,Report,Goal);

        }
    }
}
