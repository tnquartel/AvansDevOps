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
        public void NextState(Item item)
        {
            item.State.NextState();
        }

        public void AssignDev(Item item, User user)
        {
            if (item.User != null)
            {

            }
        }
    }
}
