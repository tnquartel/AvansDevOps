using Domain.Core.Entities;
using Domain.Services.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Observer
{
    public class Subject : ISubject
    {
        public Dictionary<IObserver,string> Observers {  get; private set; }

        public Subject() { 
            this.Observers = new Dictionary<IObserver, string   >();
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
