using Domain.Core.Entities;
using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryDevPipeRepository : IDevPipeRepository
    {

        private List<DevPipe> DevPipes = new List<DevPipe>();

        public void Create(DevPipe newDevPipe)
        {
            DevPipes.Add(newDevPipe);
        }

        public void Delete(int id)
        {
            var x = from devPipe in DevPipes where devPipe.Id == id select devPipe;
            DevPipes.Remove(x.First());
        }

        public List<DevPipe> GetAll()
        {
            return DevPipes;
        }

        public DevPipe GetOne(int id)
        {
            var x = from devPipe in DevPipes where devPipe.Id == id select devPipe;
            return x.First();
        }

        public void Update(int id, DevPipe update)
        {
            var x = DevPipes.FirstOrDefault(x => x.Id == id);
            x = update;
        }
    }
}
