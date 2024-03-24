using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Sprint
{
    public class PartialSprint : ISprint
    {
        public int Id { get; set; }
        public List<IBacklog> Backlogs { get; set; }
        public Report Report { get; set; }
        public ISubject Subject { get; set; }
        public User? Scrummaster { get; set; }
        public string Goal {  get; }
        public List<User> Users { get; set ; }
        public Project Project { get; set; }
        public ISprintState State { get; set; }
        public DevPipe? DevPipe { get; set; }
        public PartialSprint( Report report, string goal, ISprintState state ,ISubject subject,Project project) { 
            Project = project;
            Report = report;
            Goal = goal;
            Users = new List<User>();
            Backlogs = new List<IBacklog>();
            Subject = subject;
            State = state;
        }
    }
}
