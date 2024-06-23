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
        IItemRepository _repository;
        ProjectService _projectService;
        IUserService _userService;

        public ItemService(IItemRepository itemRepository, ProjectService projectService, IUserService userService)
        {
            _repository = itemRepository;
            _projectService = projectService;
            _userService = userService;
        }
        public Item CreateItem(string name)
        {
            var y = new ToDo();
            var x = new Item(y);
            x.Name = name;
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
            _projectService.OnItemStateChanged(item);
        }

        public void FailTest(Item item)
        {
            if (item.State.GetState() is Testing testingState)
            {
                testingState.Failed();
                _projectService.TestFailedNotification(item);
            }
        }

        public void Rejected(Item item)
        {
            if (item.State.GetState() is Tested testedState)
            {
                testedState.Rejected();
            }
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
