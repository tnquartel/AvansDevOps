using Domain.Services.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Observer
{
    public interface ISubject
    {
        //Subscriber part of Observer pattern
        public Dictionary<IObserver,string> Observers { get; }
        public void Subscribe(string Type, IObserver observer);
        public void Unsubscribe(string Type, IObserver observer);
        public void Notify(string Type, string Message);
    }
}
