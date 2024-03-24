using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Sprint;


namespace Domain.Services.Repositories
{
    public interface ISprintRepository
    {
        public ISprint GetOne(int id);
        public List<ISprint> GetAll();
        public void Delete(int id);
        public void Update(int id, ISprint update);
        public void Create(ISprint NewSprint);
    }
}
