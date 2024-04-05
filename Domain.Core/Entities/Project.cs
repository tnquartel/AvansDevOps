using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public User ProductOwner { get; set; } 
        public List<User> Developers { get; set; }
        public List<Item> Items { get; set; }
        public List<ISprint> Sprints { get; set; }

        public Project(string name, User productOwner) {
            Name = name;
            ProductOwner = productOwner;
            Developers = new List<User>();
            Items = new List<Item>();
            Sprints = new List<ISprint>();
        }
    }
}
