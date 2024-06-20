using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface ISprintService
    {
        public ISprint NewSprint(ISprintFactory sprintFactory, Project project, string goal, ISubject subject);
    }
}
