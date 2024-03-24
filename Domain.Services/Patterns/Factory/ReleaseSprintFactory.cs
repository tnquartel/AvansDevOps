using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;

namespace Domain.Services.Patterns.Factory
{
    class ReleaseSprintFactory : ISprintFactory
    {
        
        public virtual ISprint CreateSprint(int Id, Report Report, string Goal, DevPipe Pipe)
        {
            Console.WriteLine("Created Release Sprint");

            return new ReleaseSprint( Pipe, Id, Report,  Goal);
        }
    }
}
