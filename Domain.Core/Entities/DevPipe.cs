using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class DevPipe
    {
        public int Id { get; set; }
        public ISubject Subject { get; set; }
        public User Scrummaster { get; set; }
        public ISprint Sprint { get; set; }
        public DevPipe(User scrummaster, ISprint sprint, ISubject subject) {
            Scrummaster = scrummaster;
            Sprint = sprint;
            Subject = subject;
        }
    }
}
