using Domain.Core.Entities.Sprint;
using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IDevPipeService
    {
        public DevPipe NewDevPipe(ReleaseSprint releaseSprint)
    }
}
