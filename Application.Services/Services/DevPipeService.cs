using Domain.Core.Entities.Sprint;
using Domain.Core.Entities;
using Domain.Services.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.Repositories;
using Domain.Services.Services;
using Domain.Services.Patterns.State.Sprint;

namespace Application.Services.Services
{
    public class DevPipeService : IDevPipeService
    {

        IDevPipeRepository _repository;
        public DevPipeService( IDevPipeRepository devPipeRepository) {
            _repository = devPipeRepository;
        }


        public DevPipe NewDevPipe(ISprint releaseSprint)
        {
            if (releaseSprint.DevPipe != null)
            {
                throw new Exception("Sprint Already has DevPipe"); 
            }
            if (releaseSprint.Scrummaster == null) 
            {
                throw new Exception("Sprint has no Scrummaster");
            }
            else
            {
                // DevPipe is observable
                var x = new DevPipe(releaseSprint.Scrummaster, releaseSprint, new Subject());
                _repository.Create(x);
                return x;
            }
        }

        public void Start(DevPipe devPipe, User user)
        {
            if (devPipe.Sprint.Scrummaster == user)
            {
                Console.WriteLine("Started development pipeling");
            }
            else
            {
                Console.WriteLine("Only the scrummaster can start the Development Pipeline");
            }
        }

        // Implements Observer Pattern
        public void Observe(IObserver observer, DevPipe devPipe)
        {
            if(devPipe.Subject != null)
            {
                devPipe.Subject.Subscribe("Test Failed", observer);

            }
            else
            {
                // This Route should never be reached, as DevPipes should be created with a Subject.
                Console.WriteLine("Something's Wrong, I can feel it");
            }
        }
        // FR - 08

        public void Restart(DevPipe devPipe, User user)
        {
            if (devPipe.Sprint.Scrummaster == user)
            {
                Console.WriteLine("Restarted development pipeling");
            }
            else
            {
                Console.WriteLine("Only the scrummaster can restart the Development Pipeline");
            }
        }

        public void Cancel(DevPipe devPipe, User user)
        {
            if (devPipe.Sprint.Scrummaster == user)
            {
                devPipe.Sprint.DevPipe = null;
            }
            else
            {
                Console.WriteLine("Only the scrummaster can cancel the Development Pipeline");
            }
        }

        public void Failure(DevPipe devPipe)
        {
            if (devPipe.Sprint.State.GetState() is Finished)
            {
                devPipe.Subject.Notify("Scrummaster", "Tests failed");
            }
            else
            {
                Console.WriteLine("Release sprint is not valid");
            }
        }

        public void End(DevPipe devPipe)
        {
            if (devPipe.Sprint.State.GetState() is Finished)
            {
                devPipe.Sprint.State.NextState();

                devPipe.Subject.Notify("Scrummaster", "Tests succesful");
                devPipe.Subject.Notify("ProductOwner", "Tests succesful");
            }
            else
            {
                Console.WriteLine("Release sprint is not valid");
            }
        }

        public void Deploy(DevPipe devPipe, User user)
        {
            if (devPipe.Sprint.Scrummaster == user)
            {
                Console.WriteLine("Sprint Deployed");
            }
            else
            {
                Console.WriteLine("Only the scrummaster can cancel the Development Pipeline");
            }
        }

        public void PubliciseTests(DevPipe devPipe, User user)
        {
            if (devPipe.Sprint.Scrummaster == user)
            {
                Console.WriteLine("Tests Published");
            }
            else
            {
                Console.WriteLine("Only the scrummaster can cancel the Development Pipeline");
            }
        }

    }
}
