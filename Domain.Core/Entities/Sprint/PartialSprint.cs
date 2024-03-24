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

        public Report Report { get; }

        public string Goal {  get; }

        public List<User> Users { get; set ; }

        public PartialSprint(int Id, Report Report, string Goal) { 
            this.Id = Id;
            this.Report = Report;
            this.Goal = Goal;
            this.Users = new List<User>();
            this.Backlogs = new List<IBacklog>();

        }
    }
}
