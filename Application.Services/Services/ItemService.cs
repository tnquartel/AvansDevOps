using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Patterns.State.ItemStates;
using Domain.Services.Repositories;
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
        IItemRepository _repository;
        public ItemService(IItemRepository itemRepository) {
            _repository = itemRepository;
        }
        public Item CreateItem()
        {
            var y = new ToDo();
            var x = new Item(y);
            y.Item = x;
            _repository.Create(x);
            return x;

        }

        //FR - 02
        public void AddActivity(Item item, ActivityItem activity) 
        {
            if (!item.Activities.Contains(activity))
            {
                item.Activities.Add(activity);
            }
            else 
            {
                Console.WriteLine("Item can't have duplicate activities");
            }
        }

        // Implements State Pattern
        public void NextState(Item item)
        {
            item.State.NextState();
        }

        public void AssignDev(Item item, User user)
        {
            if (item.User != null)
            {
                item.User = user;
            }
        }


    }
}
