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
        public UserService UserService = new UserService();
        public IAuthService AuthService = new AuthService();
        public ItemService(IItemRepository itemRepository) {
            _repository = itemRepository;
        }
        public Item CreateItem(Project project)
        {
            var y = new ToDo();
            var x = new Item(y, project);
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
            if (item.State.GetState() is Tested)
            {
                if (AuthService.GetLoggedInUser != null && item.Sprint.Scrummaster.Equals(AuthService.GetLoggedInUser))
                {
                    bool alldone = true;
                    foreach (var activity in item.Activities)
                    {
                        if (!activity.isDone)
                        {
                            alldone = false;
                            break;
                        }
                    }
                    if (alldone)
                    {
                        item.State.NextState();
                    }
                }
            } else
            {
                item.State.NextState();
            }
        }

        public void TestAgain(Item item)
        {
            if (item.State.GetState() is Tested && AuthService.GetLoggedInUser != null && item.Sprint.Scrummaster.Equals(AuthService.GetLoggedInUser)) 
            {
                ((Tested)item.State).TestAgain();
            }
        }

        public void FailedTest(Item item)
        {
            if (item.State.GetState() is Testing)
            {
                ((Testing)item.State).Failed();
            }
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
