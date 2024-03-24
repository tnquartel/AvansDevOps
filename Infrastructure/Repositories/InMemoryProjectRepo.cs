using Domain.Core.Entities;
using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class InMemoryProjectRepo : IProjectRepository
    {
        private List<Project> y = new List<Project>();

        public void Create(Project newProject)
        {
            y.Add(newProject);
        }

        public void Delete(int id)
        {
            var x = from project in y where project.Id == id select project;
            y.Remove(x.First());
        }

        public List<Project> GetAll()
        {
            return y;
        }

        public Project GetOne(int id)
        {
            var x = from project in y where project.Id == id select project;
            return x.First();
        }

        public void Update(int id, Project update)
        {
            var x = y.FirstOrDefault(x => x.Id == id);
            x = update;
        }
    }
}
