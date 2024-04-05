using Domain.Core.Entities;
using Domain.Services.Patterns.State.ItemStates;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class ThreadService : IThreadService
    {
        public void NewMessage(string message, User user, MessageThread messageThread)
        {
            if (messageThread.ParentItem != null && !(messageThread.ParentItem.State.GetState() is Done) || messageThread.ParentActivityItem != null && !(messageThread.ParentActivityItem.parent.State.GetState() is Done))
            {
                Message newMessage = new Message(message, user);
                messageThread.Messages.Add(newMessage);
                if (!messageThread.Particapants.Contains(user)) 
                {
                    messageThread.Particapants.Add(user);
                }

            } else
            {
                Console.WriteLine("Item or Activity is in state done.");
            }
        }

    }
}
