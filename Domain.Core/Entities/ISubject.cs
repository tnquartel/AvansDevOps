using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public interface ISubject
    {
        //Subscriber part of Observer pattern
        public Dictionary<IObserver, string> Observers { get; }
        public void Subscribe(string type, IObserver observer);
        public void Unsubscribe(string type, IObserver observer);
        public void Notify(string type, string message);
    }
}
