using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; private set; }
        public string Password { get; set; }
        public Item CoupledItem { get; set; }
        public ActivityItem CoupledActivityItem { get; set; }

    }
}
