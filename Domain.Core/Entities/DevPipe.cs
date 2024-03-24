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
        public ReleaseSprint ReleaseSprint { get; set; }
        public DevPipe(User scrummaster, ReleaseSprint releaseSprint, ISubject subject) {
            Scrummaster = scrummaster;
            ReleaseSprint = releaseSprint;
            Subject = subject;
        }
    }
}
