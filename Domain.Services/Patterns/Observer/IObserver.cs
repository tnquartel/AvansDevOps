﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Observer
{
    public interface IObserver
    {
        //Observer part of Observer pattern
        public void Update(string Message);
    }
}
