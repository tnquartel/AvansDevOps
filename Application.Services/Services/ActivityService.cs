using Domain.Core.Entities.Backlog;
using Domain.Core.Entities;
using Domain.Services.Services;
using System;

namespace Application.Services.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUserService _userService;

        public ActivityService(IUserService userService)
        {
            _userService = userService;
        }

        public void AssignDev(ActivityItem activity, User user)
        {
            _userService.CoupleToFirstAvailable(activity, user);
        }

        public void NewThread(ActivityItem activity)
        {
            if (activity.Thread == null)
            {
                activity.Thread = new MessageThread
                {
                    ParentActivityItem = activity
                };
            }
            else
            {
                Console.WriteLine("This activity already contains a thread.");
            }
        }
    }
}
