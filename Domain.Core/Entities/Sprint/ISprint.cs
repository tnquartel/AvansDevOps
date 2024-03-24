﻿using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Sprint
{
    public interface ISprint
    {
        public int Id { get; }
        public List<IBacklog> Backlogs { get; set; }
        public Report Report { get; }
        public String Goal { get; }
        public List<User> Users { get; set; }

    }
}
