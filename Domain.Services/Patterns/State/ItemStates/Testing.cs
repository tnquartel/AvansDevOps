﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Backlog;

namespace Domain.Services.Patterns.State.ItemStates
{
    public class Testing : IStateItem
    {
        public Item Item { get; set; }

        public Testing(Item item)
        {
            Item = item;
        }

        public void NextState()
        {
            Item.State = new Tested(Item);
        }

        public IStateItem GetState()
        {
            return this;
        }

        public void Failed()
        {
            var x = new ToDo();
            Item.State = x;
            x.Item = Item;
        }
    }
}
