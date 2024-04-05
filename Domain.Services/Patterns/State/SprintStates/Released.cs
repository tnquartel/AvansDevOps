using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.State.Sprint
{
    public class Released : ISprintState
    {
        public ISprint Sprint { get; set; }

        public Released(ISprint sprint)
        {
            Sprint = sprint;
        }

        public void NextState()
        {
            var x = new InDevelopment();
            x.Sprint = Sprint;
            Sprint.State = x;
        }

        public ISprintState GetState()
        {
            return this;
        }
    }
}
