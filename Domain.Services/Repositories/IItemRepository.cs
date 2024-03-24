using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IItemRepository
    {
        public Item GetOne(int id);
        public List<Item> GetAll();
        public void Update(int id, Item update);
        public void Delete(int id);
        public void Create(Item newDevPipe);
    }
}
