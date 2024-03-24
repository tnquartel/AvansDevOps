﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Backlog
{
    public class Activity : IBacklog
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public Thread? Thread { get; set; }
    }
}
