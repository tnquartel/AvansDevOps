using Domain.Core.Entities.Sprint;
using Domain.Core.Entities;
using Domain.Services.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.Repositories;

namespace Application.Services.Services
{
    public class DevPipeService
    {

        IDevPipeRepository _repository;
        public DevPipeService( IDevPipeRepository devPipeRepository) {
            _repository = devPipeRepository;
        }


        public DevPipe NewDevPipe(ReleaseSprint releaseSprint)
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

        // Implements Observer Pattern
        public void Observe(IObserver observer, DevPipe devPipe)
        {
            if(devPipe.Subject != null)
            {
                devPipe.Subject.Subscribe("Test Failed", observer);

            }
            else
            {
                // This Route should never be reached, as DevPipes are created wit
                Console.WriteLine("Something's Wrong, I can feel it");
            }

        }
    }
}
