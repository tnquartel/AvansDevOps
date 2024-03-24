using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.State.Sprint
{
    public class Finished : ISprintState
    {
        public ISprint Sprint { get; set; }

        public Finished(ISprint sprint) {
            Sprint = sprint;
        }

        public void NextState() {
            Sprint.State = new Released(Sprint); 
        }

        public void PreviousState()
        {
            var y = new InDevelopment();
            Sprint.State = y;
            y.Sprint = Sprint;
        }

        public ISprintState GetState()
        {
            return this;
        }
    }
}
