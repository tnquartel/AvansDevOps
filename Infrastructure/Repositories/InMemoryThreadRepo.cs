using Domain.Core.Entities;
using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryThreadRepo : IThreadRepository
    {
        private List<MessageThread> y = new List<MessageThread>();

        public void Create(MessageThread newDevPipe)
        {
            y.Add(newDevPipe);
        }

        public void Delete(int id)
        {
            var x = from thread in y where thread.Id == id select thread;
            y.Remove(x.First());
        }

        public List<MessageThread> GetAll()
        {
            return y;
        }

        public MessageThread GetOne(int id)
        {
            var x = from thread in y where thread.Id == id select thread;
            return x.First();
        }

        public void Update(int id, MessageThread update)
        {
            var x = y.FirstOrDefault(x => x.Id == id);
            x = update;
        }
    }
}
