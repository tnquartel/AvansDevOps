using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class ItemService : IItemService
    {
        public UserService UserService = new UserService();

        // Implements State Pattern
        public void NextState(Item item)
        {
            item.State.NextState();
        }

        public void AssignDev(Item item, User user)
        {
            UserService.CoupleToFirstAvailable(item, user);
        }

        public void NewThread(Item item)
        {
            if (item.Thread == null)
            {
                item.Thread = new MessageThread();
                item.Thread.ParentItem = item;
            }
            else
            {
                Console.WriteLine("This item already contains a thread.");
            }
        }
    }
}
