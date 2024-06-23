using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class MessageThread : ISubject
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Particapants { get; set; }
        public Item ParentItem { get; set; }
        public ActivityItem ParentActivityItem { get; set; }

        public MessageThread()
        {
            this.Observers = new Dictionary<IObserver, string>();
        }

        public Dictionary<IObserver, string> Observers { get; private set; }

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
    }
}
