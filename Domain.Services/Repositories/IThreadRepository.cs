using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IThreadRepository
    {
        public MessageThread GetOne(int id);
        public List<MessageThread> GetAll();
        public void Update(int id, MessageThread update);
        public void Delete(int id);
        public void Create(MessageThread newDevPipe);
    }
}
