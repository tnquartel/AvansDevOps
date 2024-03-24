using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IActivityRepository
    {
        public ActivityItem GetOne(int id);
        public List<ActivityItem> GetAll();
        public void Update(int id, ActivityItem update);
        public void Delete(int id);
        public void Create(ActivityItem newDevPipe);
    }
}
