using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class InMemoryActivityRepo : IActivityRepository
    {
        private List<ActivityItem> x = new List<ActivityItem>();

        public void Create(ActivityItem newActivity)
        {
            x.Add(newActivity);
        }

        public void Delete(int id)
        {
            var y = from activity in x where activity.Id == id select activity;
            x.Remove(y.First());
        }

        public List<ActivityItem> GetAll()
        {
            return x;
        }

        public ActivityItem GetOne(int id)
        {
            var y = from activity in x where activity.Id == id select activity;
            return y.First();
        }

        public void Update(int id, ActivityItem update)
        {
            var y = x.FirstOrDefault(y => y.Id == id);
            y = update;
        }
    }
}
