using Domain.Core.Entities.Backlog;
using Domain.Core.Entities;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class ActivityService : IActivityService
    {
        public UserService UserService = new UserService();

        public void AssignDev(ActivityItem activity, User user)
        {
            UserService.CoupleToFirstAvailable(activity, user);
        }

        public void isDone(ActivityItem activity)
        {
            activity.isDone = true;
        }

        public void NewThread(ActivityItem activity) 
        {
            if (activity.Thread == null)
            {
                activity.Thread = new MessageThread();
                activity.Thread.ParentActivityItem = activity;
            } else
            {
                Console.WriteLine("This activity already contains a thread.");
            }
        }
    }
}
