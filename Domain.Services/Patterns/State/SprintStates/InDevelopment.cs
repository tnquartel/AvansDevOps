using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.State.Sprint
{
    public class InDevelopment : ISprintState
    {
        public ReleaseSprint Sprint { get; set; }

        public InDevelopment(ReleaseSprint sprint)
        {
            Sprint = sprint;
        }

        public void NextState()
        {
            Sprint.State = new Finished(Sprint);
        }

    }
}
