using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface ISprintService
    {
        public ISprint NewSprint(string type, Project project);
    }
}
