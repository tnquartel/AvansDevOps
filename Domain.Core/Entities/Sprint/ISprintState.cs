﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities.Sprint
{
    public interface ISprintState
    {
        // State Interface for State Pattern
        public ISprint Sprint { get; set; }

        public void NextState();

        public ISprintState GetState();
    }
}
