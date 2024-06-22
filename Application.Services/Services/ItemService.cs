using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Patterns.State.ItemStates;
using Domain.Services.Repositories;
using Domain.Services.Services;
using System;

namespace Application.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IUserService _userService;

        public ItemService(IItemRepository itemRepository, IUserService userService)
        {
            _repository = itemRepository;
            _userService = userService;
        }

        public Item CreateItem()
        {
            var y = new ToDo();
            var x = new Item(y);
            y.Item = x;
            _repository.Create(x);
            return x;
        }

        // FR - 02
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
            _userService.CoupleToFirstAvailable(item, user);
        }

        public void NewThread(Item item)
        {
            if (item.Thread == null)
            {
                item.Thread = new MessageThread
                {
                    ParentItem = item
                };
            }
            else
            {
                Console.WriteLine("This item already contains a thread.");
            }
        }
    }
}
