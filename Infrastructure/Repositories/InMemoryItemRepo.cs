using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryItemRepo : IItemRepository
    {
        private List<Item> Items = new List<Item>();

        public void Create(Item newItem)
        {
            Items.Add(newItem);
        }

        public void Delete(int id)
        {
            var x = from item in Items where item.Id == id select item;
            Items.Remove(x.First());
        }

        public List<Item> GetAll()
        {
            return Items;
        }

        public Item GetOne(int id)
        {
            var x = from item in Items where item.Id == id select Item;
            return x.First();
        }

        public void Update(int id, Item update)
        {
            var x = Items.FirstOrDefault(x => x.Id == id);
            x = update;
        }
    }
}
