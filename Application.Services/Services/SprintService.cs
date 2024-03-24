using Domain.Core.Entities;
using Domain.Services.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    internal class SprintService
    {
        private ISubject Subject { get; set; }
        public User Scrummaster { get; private set; }
        private List<User> Developers { get; set; }

        public SprintService(User Scrummaster)
        {
            Subject = new Subject();
            Developers = new List<User>();
            this.Scrummaster = Scrummaster;
            
        }

        public void Observe(IObserver observer)
        {
            Subject.Subscribe("Test Failed", observer);
        }

        public void TestFailure()
        {
            Subject.Notify("Tests Failed", "The Tests have failed");
        }

        public void AddDeveloper(User user)
        {
            if (!Developers.Contains(user))
            {
                Developers.Add(user);
            }
        }
    }
}
