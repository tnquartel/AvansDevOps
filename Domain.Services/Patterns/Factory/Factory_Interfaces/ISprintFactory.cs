﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;

namespace Domain.Services.Patterns.Factory.Factory_Interfaces
{

    // Interface for Factory pattern to create sprints.
    interface ISprintFactory
    {
        public ISprint CreateSprint(int Id, Report Report, string Goal, DevPipe DevPipe);
    }
}
