using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.State.ItemStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.State.Sprint
{
    public class InDevelopment : ISprintState
    {
        public ISprint? Sprint { get; set; }

        public InDevelopment()
        {
        }

        public void NextState()
        {
            if (Sprint != null)
            {
                Sprint.State = new Finished(Sprint);
            }
            else
            {
                Console.WriteLine("States Disconnected");
            }
        }
        public ISprintState GetState()
        {
            return this;
        }

    }
}
