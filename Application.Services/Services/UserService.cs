using Domain.Services.Services;
using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Backlog;
using System.Diagnostics;
using System.Xml.Linq;
using Domain.Services.Patterns.State.ItemStates;

namespace Application.Services.Services
{
    public class UserService : IUserService
    {

        public void SendMessage(User user, string message)
        {
            Console.WriteLine(user.Name + " : " + message);
        }

        public void CoupleToFirstAvailable(Item item, User user)
        {
            if (!IsCoupled(user))
            {
                user.CoupledItem = item;
                user.CoupledActivityItem = null;
                Console.WriteLine($"{user.Name} is now coupled to item: {item.Id}.");
            }
            else
            {
                Console.WriteLine($"{user.Name} is already coupled to an item or activity.");
            }
        }

        public void CoupleToFirstAvailable(ActivityItem activity, User user)
        {
            if (!IsCoupled(user))
            {
                user.CoupledActivityItem = activity;
                user.CoupledItem = null;
                Console.WriteLine($"{user.Name} is now coupled to activity: {activity.Id}.");
            }
            else
            {
                Console.WriteLine($"{user.Name} is already coupled to an item or activity.");
            }
        }

        public bool IsCoupled(User user)
        {
            if (user.CoupledItem == null && user.CoupledActivityItem == null || user.CoupledItem != null && user.CoupledItem.State.GetState() is Done || user.CoupledActivityItem != null && user.CoupledActivityItem.parent.State.GetState() is Done)        
            {
                return true;
            }
            return false;
        }

    }
}
