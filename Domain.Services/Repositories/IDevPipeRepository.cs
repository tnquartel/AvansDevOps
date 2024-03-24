using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IDevPipeRepository
    {
        public DevPipe GetOne(int id);
        public List<DevPipe> GetAll();
        public void Update(int id, DevPipe update);
        public void Delete(int id);
        public void Create(DevPipe newDevPipe);

    }
}
