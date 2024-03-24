using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Sprint
{
    public class ReleaseSprint : ISprint
    {
        public int Id { get; set; }
        public List<IBacklog> Backlogs { get; set; }
        public Report Report { get;}
        public string Goal {get;}
        public List<User> Users {get; set;}
        public DevPipe? DevPipe { get; set; }
        public ISubject Subject { get; set; }
        public User? Scrummaster { get; set; }
        public ISprintState State { get; set; }
        public ReleaseSprint(int Id, Report Report, string Goal, ISprintState state, ISubject subject) {
            this.Id = Id;
            this.Report = Report;
            this.Goal = Goal;
            this.Users = new List<User>();
            this.Backlogs = new List<IBacklog>();
            State = state;
            Subject = subject;
        }
    }
}
