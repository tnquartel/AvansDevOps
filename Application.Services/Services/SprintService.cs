using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Observer;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    internal class SprintService : ISprintService
    {

        public void NewSprint()
        {

        }

        // Implements Observer Pattern
        public void Observe(IObserver observer, ISprint sprint)
        {
            sprint.Subject.Subscribe("Test Failed", observer);
        }

        public void TestFailure(ISprint sprint)
        {
            sprint.Subject.Notify("Tests Failed", "The Tests have failed");
        }

        public void AddDeveloper(User user, ISprint sprint)
        {
            if (!sprint.Users.Contains(user))
            {
                sprint.Users.Add(user);
            }
        }

        public void AddScrummaster(User user, ISprint sprint)
        {
            if (sprint.Scrummaster != null)
            {
                Console.WriteLine("Sprint already has a Scrummaster");
            } 
            else
            {
                sprint.Scrummaster = user;
            }
        }

        // Implements State Pattern

        public void NextState(ReleaseSprint sprint) {
            sprint.State.NextState();
        }

    }
}
