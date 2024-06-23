using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IProjectRepository
    {
        public Project GetOne(int id);
        public List<Project> GetAll();
        public void Update(int id, Project update);
        public void Delete(int id);
        public void Create(Project newProject);
    }
}
