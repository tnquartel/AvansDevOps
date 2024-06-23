using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Sprint;
using Domain.Services;
using Domain.Services.Repositories;

namespace Infrastructure.Repositories
{
    class InMemorySprintRepo : ISprintRepository
    {

        public List<ISprint> Sprints = new List<ISprint>();

        public void Create(ISprint newSprint)
        {
            Sprints.Add(newSprint);
        }

        public void Delete(int id)
        {
            if(Sprints.Count > 0)
            {
                var x = from Sprint in Sprints where Sprint.Id == id select Sprint;
                Sprints.Remove(x.First());
            }
        }

        public List<ISprint> GetAll()
        {
            return Sprints.ToList();
        }

        public ISprint GetOne(int id)
        {
            var x = from Sprint in Sprints where Sprint.Id == id select Sprint;
            return x.First();
        }

        public void Update(int id, ISprint update)
        {
            Sprints[id] = update;
        }
    }
}
