using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Project : ISubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User ProductOwner { get; set; }
        public User? ScrumMaster { get; set; }
        public List<User> Developers { get; set; }
        public List<Item> Items { get; set; }
        public List<ISprint> Sprints { get; set; }


        public Project(string name, User productOwner)
        {
            Name = name;
            ProductOwner = productOwner;
            Developers = new List<User>();
            Items = new List<Item>();
            Sprints = new List<ISprint>();
            this.Observers = new Dictionary<IObserver, string>();
            Subscribe(productOwner.PreferedNotificationType, productOwner);
        }

        public Dictionary<IObserver, string> Observers { get; private set; }

        public void Subscribe(string type, IObserver observer)
        {
            if (!this.Observers.ContainsKey(observer))
            {
                Observers.Add(observer, type);
            }
            else
            {
                Console.WriteLine("Observer already exists.");
            }
        }

        public void Unsubscribe(string type, IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify(string type, string message)
        {
            foreach (var observer in this.Observers)
            {
                if (observer.Value == type)
                {
                    observer.Key.Update(message);
                }
            }
        }
    }
}
